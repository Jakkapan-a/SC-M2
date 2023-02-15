using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TCapture
{
    public class Capture
    {
        private Thread _thread;
        private OpenCvSharp.VideoCapture _videoCapture;
        
        public delegate void VideoCaptureError(string messages);
        public event VideoCaptureError OnError;

        public delegate void VideoFrameHeadler(Bitmap bitmap);
        public event VideoFrameHeadler OnFrameHeadler;
        
        public delegate void VideoCaptureStop();
        public event VideoCaptureStop OnVideoStop;

        public delegate void VideoCaptureStarted();
        public event VideoCaptureStarted OnVideoStarted;

        private bool _onStarted = false;

        public bool _isRunning { get; set; }

        private int _frameRate = 50;

        public int width { get; set; }
        public int height { get; set; }
        public int frameRate
        {
            get { return _frameRate; }
            set { _frameRate = 1000 / value; }
        }

        public bool IsOpened
        {
            get { return IsOpen(); }
        }
        public bool IsOpen()
        {
            if (_videoCapture != null && _videoCapture.IsOpened())
            {
                return true;
            }
            return false;
        }

        public Capture()
        {
            width = 1280;
            height = 720;
        }
        public void Start(int device)
        {
            if (_videoCapture != null)
            {
                _videoCapture.Dispose();
            }

            _videoCapture = new OpenCvSharp.VideoCapture(device);
            _videoCapture.Open(device);
            SetFrame(width, height);
            _isRunning = true;
            _onStarted = true;
            if (_thread != null)
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
                    if (_videoCapture.IsOpened())
                    {
                        using (OpenCvSharp.Mat frame = _videoCapture.RetrieveMat())
                        {
                            if (frame.Empty())
                            {
                                OnError?.Invoke("Frame is empty");
                                continue;
                            }
                            using (Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame))
                            {
                                OnFrameHeadler?.Invoke(bitmap);
                            }
                        }
                        if (_onStarted)
                        {
                            OnVideoStarted?.Invoke();
                            _onStarted = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(ex.Message);
                }
                Thread.Sleep(_frameRate);
            }
        }

        public void SetFrame(int width, int height)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, width);
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, height);
        }

        public void Stop()
        {
            _isRunning = false;
            if (_videoCapture != null)
            {
                _videoCapture.Release();
            }
            OnVideoStop?.Invoke();
        }

        public void Resumed()
        {
            _isRunning = true;
            if (_thread != null)
            {
                _thread.Abort();
            }
            _thread = new Thread(FrameCapture);
            _thread.Start();
        }

        public void Dispose()
        {
            _isRunning = false;
            if (_videoCapture != null)
            {
                _videoCapture.Dispose();
            }
            if (_thread != null)
            {
                _thread.Abort();
            }
        }

        // Get Focus
        public int GetFocus()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Focus);
        }
        // Set Focus 
        public void SetFocus(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Focus, value);
        }
        // Auto Focus
        public void AutoFocus()
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Focus, -1);
        }

        // Get Zoom
        public int GetZoom()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Zoom);
        }

        // Set Zoom
        public void SetZoom(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Zoom, value);
        }
        // Get Exposure
        public int GetExposure()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Exposure);
        }

        // Set Exposure
        public void SetExposure(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Exposure, value);
        }

        // Get Gain
        public int GetGain()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Gain);
        }

        // Set Gain
        public void SetGain(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Gain, value);
        }

        // Set Brightness
        public void SetBrightness(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Brightness, value);
        }

        // Set Contrast
        public void SetContrast(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Contrast, value);
        }

        // Set Saturation
        public void SetSaturation(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Saturation, value);
        }

    }
}
