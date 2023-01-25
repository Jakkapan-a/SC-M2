using Emgu.CV.Ocl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SC_M2_V2._00.Modules
{
    public class VideoCAM
    {
        private Thread _thread;
        private OpenCvSharp.VideoCapture _videoCapture;

        public delegate void VideoCaptureError(string message);
        public event VideoCaptureError OnVideoError;
        public delegate void VideoCaptureStop();
        public event VideoCaptureStop OnVideoStop;

        // VideoFrameHandler
        public delegate void VideoFrameHandler(Bitmap bitmap);
        public event VideoFrameHandler OnVideoFrameHandler;
        
        private bool _isRunning = false;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        public bool IsOpened
        {
            get { return IsOpen(); }
        }

        public bool IsOpen()
        {
            if(_videoCapture!= null && _videoCapture.IsOpened())
            {
                return true;
            }
            return false;
        }
        public void Start(int device)
        {
            if(_videoCapture != null)
            {
                _videoCapture.Dispose();
            }

            _videoCapture = new OpenCvSharp.VideoCapture(device);
            _videoCapture.Open(device);
            SetResolution(1280, 720);
            _isRunning = true;

            if(_thread != null)
            {
                _thread.Abort();
            }
            _thread = new Thread(FrameCapture);
            _thread.Start();
        }
      
        private void FrameCapture()
        {
            while (_isRunning)
            {
                try
                {
                    using (OpenCvSharp.Mat frame = _videoCapture.RetrieveMat())
                    {
                        if (frame.Empty())
                        {
                            OnVideoError?.Invoke("Frame is empty");
                            continue;
                        }
                        using (Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame))
                        {
                            OnVideoFrameHandler?.Invoke(bitmap);
                        }
                        //Console.WriteLine("Frame {0}", frame);
                    }
                   
                }
                catch (Exception ex)
                {
                    OnVideoError?.Invoke(ex.Message);
                }
                // 20 FPS
                Thread.Sleep(50);
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _videoCapture.Release();
            OnVideoStop?.Invoke();
        }

        public void Resumed()
        {
            _isRunning = true;
            if(_thread != null)
            {
                _thread.Abort();
            }
            _thread = new Thread(FrameCapture);
            _thread.Start();
        }

        public void Dispose()
        {
            _isRunning = false;
            if(_videoCapture != null )
            {
                _videoCapture.Dispose();
            }
            if(_thread != null)
            {
                _thread.Abort();
            }
        }

        public void SetResolution(int width, int height)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, width);
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, height);
        }

        public void SetFPS(int fps)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Fps, fps);
        }
    }

   
}
