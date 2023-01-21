using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M3
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private VideoCapture _camera1;
        private VideoCapture _camera2;
        private Timer _timer;
        private void Home_Load(object sender, EventArgs e)
        {
            // Open the first camera
            _camera1 = new VideoCapture(2);
            // Open the second camera
            _camera2 = new VideoCapture(1);
            _camera1.Open(0);
            _camera2.Open(1);

            _timer = new Timer();
            // Set the interval of the timer
            _timer.Interval = 50; // 15
                                  // Attach the event handler to the timer
            _timer.Tick += new EventHandler(Timer_Tick);
            // Start the timer
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_camera1 != null && _camera1.IsOpened())
            {
                using(Mat frame= _camera1.RetrieveMat()) 
                { 
                    if(frame != null)
                    {
                        pictureBox1.SuspendLayout();
                        pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                        pictureBox1.ResumeLayout();
                    }
    
                }
            }
            if (_camera2 != null && _camera2.IsOpened())
            {
                using (Mat frame = _camera2.RetrieveMat())
                {
                    if (frame != null)
                    {
                        pictureBox2.SuspendLayout();
                        pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                        pictureBox2.ResumeLayout();
                    }
                }
            }
        }
    }
}
