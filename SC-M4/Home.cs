using DirectShowLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCapture;
using SC_M4.Properties;
using System.Drawing.Imaging;

namespace SC_M4
{
    public partial class Home : Form
    {
        public TCapture.Capture capture_1;
        public TCapture.Capture capture_2;
        private Thread thread;

        public Home()
        {
            InitializeComponent();
        }

        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };

        private void Home_Load(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            comboBoxCamera1.Items.Clear();

            foreach (DsDevice device in videoDevices)
            {
                comboBoxCamera1.Items.Add(device.Name);
                comboBoxCamera2.Items.Add(device.Name);
            }
            if (comboBoxCamera1.Items.Count > 0)
            {
                comboBoxCamera1.SelectedIndex = 0;
                comboBoxCamera2.SelectedIndex = 0;
            }

            comboBoxBaud.Items.Clear();
            comboBoxBaud.Items.AddRange(this.baudList);
            if (comboBoxBaud.Items.Count > 0)
                comboBoxBaud.SelectedIndex = comboBoxBaud.Items.Count - 1;

            comboBoxCOMPort.Items.Clear();
            comboBoxCOMPort.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxCOMPort.Items.Count > 0)
                comboBoxCOMPort.SelectedIndex = 0;

            capture_1 = new Capture();
            capture_1.OnFrameHeadler += Capture_1_OnFrameHeadler;
            capture_1.OnVideoStarted += Capture_1_OnVideoStarted;
            capture_1.OnVideoStop += Capture_1_OnVideoStop;
            capture_2 = new Capture();
            capture_2.OnFrameHeadler += Capture_2_OnFrameHeadler;
            capture_2.OnVideoStarted += Capture_2_OnVideoStarted;
            capture_2.OnVideoStop += Capture_2_OnVideoStop;

        }

        private delegate void Stop_video();

        private void Capture_2_OnVideoStop()
        {

        }

        private void Capture_2_OnVideoStarted()
        {
            Console.WriteLine("Cam 2 Started");
        }

        private delegate void FrameRate(Bitmap bitmap);

        private void Capture_2_OnFrameHeadler(Bitmap bitmap)
        {
            if (pictureBoxCamera02.InvokeRequired)
            {
                pictureBoxCamera02.Invoke(new FrameRate(Capture_2_OnFrameHeadler), bitmap);
                return;
            }

            if (!isStart)
                pictureBoxCamera02.Image = null;
            else
                pictureBoxCamera02.Image = (Image)bitmap?.Clone();
        }

        private void Capture_1_OnVideoStop()
        {
            
        }

        private void Capture_1_OnVideoStarted()
        {
            Console.WriteLine("Cam 1 Started");
        }

    
        private void Capture_1_OnFrameHeadler(Bitmap bitmap)
        {
            if(pictureBoxCamera01.InvokeRequired)
            {
                pictureBoxCamera01.Invoke(new FrameRate(Capture_1_OnFrameHeadler),bitmap);
                return;
            }
            if (!isStart)
                pictureBoxCamera01.Image = null;
            else
                pictureBoxCamera01.Image = (Image)bitmap?.Clone();
        }

        private bool isStart = false;
        private int driveindex_01 = -1;
        private int driveindex_02 = -1;

        private void btStartStop_Click(object sender, EventArgs e)
        {
            this.isStart = !this.isStart;
            try
            {
                if (this.isStart)
                {

                    if(this.comboBoxCamera1.SelectedIndex == -1 || this.comboBoxCamera2.SelectedIndex == -1)
                    {
                        throw new Exception(Properties.Resources.msg_select_camera);
                    }
                    if (capture_1.IsOpened)
                        capture_1.Stop();
                    if (capture_2.IsOpened)
                        capture_2.Stop();
                    //openCamera();
                    driveindex_01 = comboBoxCamera1.SelectedIndex;
                    driveindex_02 = comboBoxCamera2.SelectedIndex;
                    Task.Factory.StartNew(() => capture_1.Start(driveindex_01));
                    Task.Factory.StartNew(() => capture_2.Start(driveindex_02));

                    btStartStop.Text = "STOP";
                    if (thread != null)
                    {
                        thread.Abort();
                        thread.DisableComObjectEagerCleanup();
                        thread = null;
                    }

                    thread = new Thread(new ThreadStart(ProcessTesting));
                    thread.Start();

                }
                else
                {
                    if (capture_1._isRunning)
                        capture_1.Stop();
                    
                    if (capture_2._isRunning)
                        capture_2.Stop();

                    btStartStop.Text = "START";
                    pictureBoxCamera01.Image = null;
                    pictureBoxCamera02.Image = null;
                   
                    if(thread != null)
                    {
                        thread.Abort(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.isStart = false;
                btStartStop.Text = "START";
                
            }
        }

        private void ProcessTesting()
        {
            // This thread working in her
            // While loop -> detect came
            //int i = 0;
            //while (true)
            //{
            //    i++;
            //    Console.WriteLine("Test {0}",i);
            //    Thread.Sleep(100);
            //}
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(capture_1 != null)
            {
                if(capture_1._isRunning)
                {
                    capture_1.Stop();
                }
                capture_1.Dispose();
            }
            if (capture_2 != null)
            {
                if (capture_2._isRunning)
                {
                    capture_2.Stop();
                }
                capture_2.Dispose();
            }

            if(thread != null)
            {
              thread.Abort();  
            }
        }

    }
}
