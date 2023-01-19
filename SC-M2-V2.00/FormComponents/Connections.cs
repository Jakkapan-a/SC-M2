using DirectShowLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SC_M2_V2._00.Properties;
using System.IO.Ports;

namespace SC_M2_V2_00.FormComponent
{
    public partial class Connections : Form
    {
        Home home;
        public Connections(Home home)
        {
            InitializeComponent();
            this.home = home;
        }

        private void Connections_Load(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            comboBoxCamera1.Items.Clear();

            foreach (DsDevice device in videoDevices)
            {
                comboBoxCamera1.Items.Add(device.Name);
                comboBoxCamera2.Items.Add(device.Name);
            }
            if(comboBoxCamera1.Items.Count>0)
            { 
                comboBoxCamera1.SelectedIndex = 0;
                comboBoxCamera2.SelectedIndex = 0; 
            }

            comboBoxBaud.Items.Clear();
            comboBoxBaud.Items.AddRange(this.home.baudList);
            if(comboBoxBaud.Items.Count>0)
                comboBoxBaud.SelectedIndex = comboBoxBaud.Items.Count-1;

            comboBoxCOMPort.Items.Clear();
            comboBoxCOMPort.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxCOMPort.Items.Count > 0)
                comboBoxCOMPort.SelectedIndex = 0;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(comboBoxCamera1.SelectedIndex == comboBoxCamera2.SelectedIndex)
            {
                MessageBox.Show("Same drive!!", Resources.Path_System,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            this.home.deviceCamera1 = comboBoxCamera1.SelectedIndex;
            this.home.deviceCamera2 = comboBoxCamera2.SelectedIndex;

            this.home.baudrate = comboBoxBaud.SelectedItem.ToString();
            this.home.serialportName = comboBoxCOMPort.SelectedItem.ToString();

            this.home.serialConnect();

            this.home.toolStripStatusDrive.Text = $"Drive1: {comboBoxCamera1.SelectedItem.ToString()} Drive2: {comboBoxCamera2.SelectedItem.ToString()} COM Port: {comboBoxCOMPort.SelectedItem.ToString()}  Baud: {comboBoxBaud.SelectedItem.ToString()}";
            this.Close();
        }
    }
}
