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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private WindowsFrame.WindowsCapture capture;
        private WindowsFrame.WindowsCapture capture2;
        bool state = false;
        private void Form2_Load(object sender, EventArgs e)
        {
            // capture = new WindowsFrame.WindowsCapture(0);
            // capture.FrameArrived += Capture_FrameArrived;
            capture = new WindowsFrame.WindowsCapture("SC-M2-V2.00");
            capture.MessageReceived += Capture_MessageReceived;
            capture.FrameEventArgs += Capture_FrameEventArgs;
            //capture.Start(0);

            capture2 = new WindowsFrame.WindowsCapture("CAM2");
            capture2.FrameEventArgs += Capture2_FrameEventArgs;
            //capture2.Start(1);

            Task.Run(async () =>
            {
                capture.Start(0);
                await Task.Delay(100);
                capture2.Start(2);
            });
        }

        private void Capture_FrameEventArgs(object sender, WindowsFrame.FrameEventArgs args)
        {
            //Console.WriteLine("Frame TICK");
            if(args.Frame != null){
                pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(args.Frame);
            }
            //if (!state)
            //{
            //    capture2.Start(2);
            //    state = true;
            //}
        }
        private void Capture2_FrameEventArgs(object sender, WindowsFrame.FrameEventArgs args)
        {
            //Console.WriteLine("Frame 2 TICK");
            if(args.Frame != null){
                pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(args.Frame);
            }
        }
        private void Capture_MessageReceived(object sender, WindowsFrame.MessageReceivedEventArgs args)
        {
            Console.WriteLine(args.MessageId.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Task.Run(async () =>  
           {
               while(true)
               {
                   capture.FrameTick();
                   capture2.FrameTick();
                   await Task.Delay(100);
               }
               //capture.PostMessage(1, new IntPtr(0), new IntPtr(1));
            });
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
