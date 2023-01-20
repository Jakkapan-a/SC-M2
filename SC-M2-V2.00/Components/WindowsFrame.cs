using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFrame
{
    class VideoCapture : IDisposable
    {
        private delegate IntPtr WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        // Delegate to filter which windows to include 
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
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
        static extern ushort RegisterClassW([In] ref WNDCLASS lpWndClass);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpClassName, int nMaxCount);
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
        static extern IntPtr DefWindowProcW(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        const int WM_CLOSE = 0x0010;
        const int WM_DESTROY = 0x0002;
        const int WM_QUIT = 0x0012;
        const int WM_USER = 0x0400;
        const int WM_APP = 0x8000;
        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int WM_NCRBUTTONDOWN = 0x00A4;

        bool _disposed;
        const int ERROR_CLASS_ALREADY_EXISTS = 1410;
        IntPtr _handle;
        WindowProc _windowProc;
        private OpenCvSharp.VideoCapture capture;

        ~VideoCapture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }
                // Dispose unmanaged resources.
                capture.Release();
                PostMessage(_handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                PostMessage(_handle, WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
                PostMessage(_handle, WM_QUIT, IntPtr.Zero, IntPtr.Zero);
                DestroyWindow(_handle);
                _disposed = true;
            }
        }

        public VideoCapture(int device)
        {
            capture = new OpenCvSharp.VideoCapture(device);
            _windowProc = WndProc;
            WNDCLASS wc = new WNDCLASS();
            wc.lpszClassName = "OpenCvSharp.VideoCapture";
            wc.lpfnWndProc = _windowProc;
            wc.hInstance = IntPtr.Zero;
            wc.hbrBackground = IntPtr.Zero;
            wc.hCursor = IntPtr.Zero;
            wc.hIcon = IntPtr.Zero;
            wc.lpszMenuName = null;
            wc.cbClsExtra = 0;
            wc.cbWndExtra = 0;
            ushort atom = RegisterClassW(ref wc);
            if (atom == 0)
            {
                int error = Marshal.GetLastWin32Error();
                if (error != ERROR_CLASS_ALREADY_EXISTS)
                {
                    throw new Exception("RegisterClassW failed with error code " + error);
                }
            }
            _handle = CreateWindowExW(0, wc.lpszClassName, null, 0, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            if (_handle == IntPtr.Zero)
            {
                throw new Exception("CreateWindowExW failed with error code " + Marshal.GetLastWin32Error());
            }

            // Create window
            _handle = CreateWindowExW(0, wc.lpszClassName, null, 0, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
        }

        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return DefWindowProcW(hWnd, msg, wParam, lParam);
        }

        public Mat Read()
        {
            return capture.RetrieveMat();
        }

        public bool IsOpened()
        {
            return capture.IsOpened();
        }

        public void Release()
        {
            capture.Release();
        }

        public double Get(int propId)
        {
            return capture.Get(propId);
        }

        public bool Set(int propId, double value)
        {
            return capture.Set(propId, value);
        }

        public int FrameWidth
        {
            get
            {
                return capture.FrameWidth;
            }
            set { capture.FrameWidth = value; }
        }

        public int FrameHeight
        {
            get
            {
                return capture.FrameHeight;
            }
            set { capture.FrameHeight = value; }
        }

        public double Fps
        {
            get
            {
                return capture.Fps;
            }
        }

        public string FourCC
        {
            get
            {
                return capture.FourCC;
            }
        }

        public event FrameVideoEventHandler FrameArrived;
        public void Start()
        {
            if (capture.IsOpened())
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        using (Mat frame = capture.RetrieveMat())
                        {
                            if (frame != null)
                            {
                                FrameArrived?.Invoke(this, new FrameVideoEventArgs(frame));
                            }
                        }

                        Thread.Sleep(50);
                    }
                });
            }
        }

        public void Stop()
        {
            PostMessage(_handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            PostMessage(_handle, WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
            PostMessage(_handle, WM_QUIT, IntPtr.Zero, IntPtr.Zero);
            DestroyWindow(_handle);
            _handle = IntPtr.Zero;
        }
    }

    class FrameVideoEventArgs : EventArgs
    {
        public Mat Frame { get; private set; }
        public FrameVideoEventArgs(Mat frame)
        {
            Frame = frame;
        }
    }
    delegate void FrameVideoEventHandler(object sender, FrameVideoEventArgs e);
    delegate IntPtr WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
}
