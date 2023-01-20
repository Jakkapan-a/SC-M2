using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M2_V2._00.Controls
{
    public partial class Capture : PictureBox
    {

        private OpenCvSharp.VideoCapture capture;
        public int drive { get; set; }
        private bool isCapture = false;
        public Capture()
        {
            InitializeComponent();
            drive = -1;
        }
        public Capture(int index)
        {
            InitializeComponent();
            drive = index;
        }
        public void Start()
        {
            if(drive == -1)
            {
                MessageBox.Show("Drive index is emty", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            if(isCapture)
            {
                MessageBox.Show("Video is runing...", "Exclamation", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            isCapture= true;
            Task.Run(async () =>
            {
                capture = new OpenCvSharp.VideoCapture(drive);
                capture.Open(drive);
                while(isCapture)
                {
                    if (capture.IsOpened())
                    {
                        using (OpenCvSharp.Mat frame = capture.RetrieveMat())
                        {
                            this.SuspendLayout();
                            this.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                            this.ResumeLayout();
                        }
                    }                    
                    await Task.Delay(100);
                }
            });
        }

        public void Stop() 
        { 
            isCapture= false;
            Thread.Sleep(1000);
            if(capture != null)
            {
                capture.Release();
                capture.Dispose();
            }
        }
    }
}
