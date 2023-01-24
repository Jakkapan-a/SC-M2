using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SC_M2_V2._00
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //private WindowsFrame.WindowsCapture capture;
        //private WindowsFrame.WindowsCapture capture2;
        bool state = false;
        private void Form2_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //Task.Run(async () =>  
           //{
           //    while(true)
           //    {
           //        capture.FrameTick();
           //        capture2.FrameTick();
           //        await Task.Delay(100);
           //    }
           //    //capture.PostMessage(1, new IntPtr(0), new IntPtr(1));
           // });
        }

        // private void Capture_FrameArrived(object sender, WindowsFrame.FrameVideoEventArgs e)
        // {
        //     if (e.Frame != null)
        //     {
        //         pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(e.Frame);
        //     }

        //     Console.WriteLine("Video...");

        // }
    }
}
