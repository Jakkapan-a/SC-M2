using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFrame
{
    class WindowsCapture : IDisposable
    {
         // original code by MoreChilli
    // class WindowsCapture : IDisposable
    // {
        private delegate IntPtr WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        // Delegate to filter which windows to include 
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [StructLayout(
            LayoutKind.Sequential,
           CharSet = CharSet.Unicode
        )]
        private struct WNDCLASS
        {
            public uint style;
            public WindowProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszMenuName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszClassName;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern ushort RegisterClassW(
            [In] ref WNDCLASS lpWndClass
        );
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr CreateWindowExW(
           UInt32 dwExStyle,
           [MarshalAs(UnmanagedType.LPWStr)]
       string lpClassName,
           [MarshalAs(UnmanagedType.LPWStr)]
       string lpWindowName,
           UInt32 dwStyle,
           Int32 x,
           Int32 y,
           Int32 nWidth,
           Int32 nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam
        );

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr DefWindowProcW(
            IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam
        );

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyWindow(
            IntPtr hWnd
        );
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool FrameTick(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        const int WM_USER = 0x0400;

        const int ERROR_CLASS_ALREADY_EXISTS = 1410;

        bool _disposed;
        IntPtr _handle;
        WindowProc _wndProc;

        ~WindowsCapture()
        {
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // Dispose unmanaged resources
                if (_handle != IntPtr.Zero)
                {
                    DestroyWindow(_handle);
                    _handle = IntPtr.Zero;
                }
                _disposed = true;
            }
        }
        
        private OpenCvSharp.VideoCapture capture = null;
        public WindowsCapture(string className)
        {
            if (string.IsNullOrEmpty(className))
                className = "MessageWindow";
            _wndProc = WndProc;
            capture = new OpenCvSharp.VideoCapture();
            // Create WNDCLASS
            var wndclass = new WNDCLASS();
            wndclass.lpszClassName = className;
            wndclass.lpfnWndProc = _wndProc;

            var classAtom = RegisterClassW(ref wndclass);

            int lastError = Marshal.GetLastWin32Error();

            if (classAtom == 0 && lastError != ERROR_CLASS_ALREADY_EXISTS)
            {
                throw new Exception("Could not register window class");
            }

            // Create window
            _handle = CreateWindowExW(
                0,
                wndclass.lpszClassName,
                String.Empty,
                0,
                0,
                0,
                0,
                0,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero
            );
        }
        public event MessageReceivedEventHandler MessageReceived;
        public event FrameEventArgsEventHandler FrameEventArgs;
        // IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        // {
        //     if (msg >= WM_USER && msg < WM_USER + 0x8000)
        //     {
        //         var args = new MessageReceivedEventArgs(msg - WM_USER, wParam, lParam);
        //         MessageReceived?.Invoke(this, args);
        //         if (!args.Handled)
        //             return DefWindowProcW(hWnd, msg, wParam, lParam);
        //         return IntPtr.Zero;
                
        //         // if (msg == WM_USER + 1)
        //         // {
        //         //     var args = new FrameEventArgs(GetFrame());
        //         //     FrameEventArgs?.Invoke(this, args);
        //         //     if (!args.Handled)
        //         //         return DefWindowProcW(hWnd, msg, wParam, lParam);
        //         //     return IntPtr.Zero;
        //         // }
        //         Console.WriteLine("MessageReceived");
        //     }
        //     return DefWindowProcW(hWnd, msg, wParam, lParam);
        // }

        IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg >= WM_USER && msg < WM_USER + 0x8000)
            {               
                if (msg == WM_USER + 1)
                {
                    var args = new FrameEventArgs(GetFrame());
                    FrameEventArgs?.Invoke(this, args);
                    if (!args.Handled)
                        return DefWindowProcW(hWnd, msg, wParam, lParam);
                    return IntPtr.Zero;
                }
            }
            return DefWindowProcW(hWnd, msg, wParam, lParam);
        }
        public void PostMessage(int messageId, IntPtr wParam, IntPtr lParam)
        {
            PostMessage(_handle, messageId + WM_USER, wParam, lParam);
        }

        public void FrameTick()
        {
            PostMessage(_handle, 1 + WM_USER, new IntPtr(1), new IntPtr(1));
        }
        
        
        public void Start(int index){
            capture.Open(index);
        }
        public void Stop(){
            capture.Release();
        }

        public Mat GetFrame(){
            if(!capture.IsOpened())
                return null;

            return capture.RetrieveMat();
        }
        public static void PostRemoteMessage(IntPtr hWnd, int messageId, IntPtr parameter1, IntPtr parameter2)
        {
            PostMessage(hWnd, messageId + WM_USER, parameter1, parameter2);
        }
        public string ClassName {
            get {
                var sb = new StringBuilder(256);
                GetClassNameW(_handle, sb, sb.Capacity);
                return sb.ToString();
            }
        }
        public static IReadOnlyList<IntPtr> GetMessageWindowHandlesByClassName(string className)
        {
            if (string.IsNullOrEmpty(className))
                className = "MessageWindow";
            var result = new List<IntPtr>();
            var sb = new StringBuilder(256);
            EnumWindows(new EnumWindowsProc((IntPtr hWnd, IntPtr lParam) =>
            {
                GetClassNameW(hWnd, sb, sb.Capacity);
                if (className == sb.ToString())
                {
                    result.Add(hWnd);
                }
                return true;
            }), IntPtr.Zero);
            //Thread.Sleep(100);
            
            return result;
        } 
        public IntPtr Handle { get { return _handle; } }   
    }
    class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(int messageId, IntPtr parameter1, IntPtr parameter2)
        {
            MessageId = messageId;
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }
        public int MessageId { get; }
        public IntPtr Parameter1 { get; }
        public IntPtr Parameter2 { get; }
        public bool Handled { get; set; }
    }
    delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs args);

        class FrameEventArgs : EventArgs
    {
        public FrameEventArgs(Mat frame)
        {
            Frame = frame;
        }
        public Mat Frame { get; }
        public bool Handled { get; set; }
    }
    delegate void FrameEventArgsEventHandler(object sender, FrameEventArgs args);

}
