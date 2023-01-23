
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M2_V2._00
{
    public partial class Frame1 : Form
    {
        private int drive = -1;
        private OpenCvSharp.VideoCapture capture;
        public Frame1(int driveIndex)
        {
            InitializeComponent();
            drive = driveIndex;
            capture = new OpenCvSharp.VideoCapture(drive);
            capture.Open(drive);
            timerVideo.Start();
        }
        
        private void Frame1_Load(object sender, EventArgs e)
        {
           
        }

        private void timerVideo_Tick(object sender, EventArgs e)
        {
            try
            {
                if (capture != null && capture.IsOpened())
                {
                    using (OpenCvSharp.Mat frame = capture.RetrieveMat())
                    {
                        if (frame != null)
                        {
                            pictureCrop1.SuspendLayout();
                            pictureCrop1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                            pictureCrop1.ResumeLayout();

                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void Frame1_FormClosing(object sender, FormClosingEventArgs e)
        {
            capture.Release();
            timerVideo.Stop();
            timerVideo.Dispose();
        }
    }
}
