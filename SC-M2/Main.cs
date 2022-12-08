using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.IO;
using SC_M2.Modules;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace SC_M2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };

        private bool isCameraOpen = false;
        private bool isConnection = false;

        private VideoCapture capture;
        private bool IsCapture;
        
        private void Main_Load(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            foreach (var device in videoDevices)
            {
                comboBoxCameraDevice.Items.Add(device.Name);
            }
            if(comboBoxCameraDevice.Items.Count >0 )
                comboBoxCameraDevice.SelectedIndex = 0;

            comboBoxBaudRate.Items.AddRange(baudList);
            if (comboBoxBaudRate.Items.Count > 0)
                comboBoxBaudRate.SelectedIndex = 0;

            comboBoxComPort.Items.AddRange(SerialPort.GetPortNames());

            if (comboBoxComPort.Items.Count > 0)
                comboBoxComPort.SelectedIndex = 0;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {

        }

        private void DisposeCaptureResources()
        {
            if (capture != null)
            {
                //capture.Release();
                capture.Dispose();

                pictureBoxC.Image = null;
                IsCapture = false;
                timerVideo.Stop();
            }

        }

        private void timerVideo_Tick(object sender, EventArgs e)
        {

        }
    }
}
