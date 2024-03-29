﻿using DirectShowLib;
using SC_M3.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
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
        private string[] baudList = { "9600", "19200", "38400", "57600", "115200" };

        private VideoCAM videoCAM;
        private void Home_Load(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            foreach (var device in videoDevices)
            {
                comboBoxCameraDevice.Items.Add(device.Name);
            }
            if (comboBoxCameraDevice.Items.Count > 0)
                comboBoxCameraDevice.SelectedIndex = 0;

            comboBoxBaudRate.Items.AddRange(baudList);
            if (comboBoxBaudRate.Items.Count > 0)
                comboBoxBaudRate.SelectedIndex = baudList.Length - 1;
            loadSerialPort();

            videoCAM = new VideoCAM();
            videoCAM.OnVideoFrameHandler += VideoCAM_OnVideoFrameHandler;

            if (!Directory.Exists(SC_M3.Properties.Resources.paht_system))
                Directory.CreateDirectory(SC_M3.Properties.Resources.paht_system);
            if (!Directory.Exists(SC_M3.Properties.Resources.path_images))
                Directory.CreateDirectory(SC_M3.Properties.Resources.path_images);
            if (!Directory.Exists(SC_M3.Properties.Resources.path_temp))
                Directory.CreateDirectory(SC_M3.Properties.Resources.path_temp);


        }
        private delegate void FrameVideo(Bitmap bitmap);
        private void VideoCAM_OnVideoFrameHandler(Bitmap bitmap)
        {
            if (pictureBoxCamera.InvokeRequired)
            {
                pictureBoxCamera.Invoke(new FrameVideo(VideoCAM_OnVideoFrameHandler), bitmap);
            }else{
                pictureBoxCamera.Image = new Bitmap(bitmap);
            }
        }

        private void loadSerialPort()
        {
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxComPort.Items.Count > 0)
                comboBoxComPort.SelectedIndex = 0;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {

        }
        private void _KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        private Image_Detect _detect;
        private void imageMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_detect != null)
            {
                _detect.Close();
                _detect.Dispose();
            }
            _detect = new Image_Detect();
            _detect.ShowDialog();
        }

        private Setting _setting;
        private void modelsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_setting != null)
            {
                _setting.Close();
                _setting.Dispose();
            }
            _setting = new Setting();
            _setting.ShowDialog();
        }
    }
}
