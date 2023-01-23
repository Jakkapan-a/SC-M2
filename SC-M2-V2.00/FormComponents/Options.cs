using DirectShowLib;
using SC_M2_V2._00.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Set = SC_M2_V2._00.Modules.Setting;


namespace SC_M2_V2._00.FormComponents
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        List<Set> settings1 = new List<Set>();
        List<Set> settings2 = new List<Set>();

        List<Set> settings3 = new List<Set>();
        private void Options_Load(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));

            comboBoxCamera.Items.Clear();

            foreach (DsDevice device in videoDevices)
            {
                comboBoxCamera.Items.Add(device.Name);
            }
            if (comboBoxCamera.Items.Count > 0)
            {
                comboBoxCamera.SelectedIndex = 0;
            }

            if (listBoxItem.Items.Count > 0)
                listBoxItem.SelectedIndex = 0;
        }

        private void listBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {  
            if(listBoxItem.SelectedIndex == 0)
            {
                settings1 = SC_M2_V2._00.Modules.Setting.GetSetting(0);
                btnSelectQR.Visible= false;
                if (settings1[0].path_image != "")
                {
                    pictureBox1.Image = Image.FromFile(settings1[0].path_image);
                }
                else
                {
                    pictureBox1.Image = null;
                }
                lbCamera.Text = "CAMERA 1"; 
                tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[1].Width = 0;
            }
            else { 

                    settings2 = Set.GetSetting(1);
                    settings3 = Set.GetSetting(2);
                    btnSelectQR.Visible= true;

                    if (settings2[0].path_image != "")
                    {
                        pictureBox1.Image = Image.FromFile(settings2[0].path_image);
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    if (settings3[0].path_image != "")
                    {
                        pictureBox2.Image = Image.FromFile(settings3[0].path_image);
                    }
                    else
                    {
                        pictureBox2.Image = null;
                    }
                    lbCamera.Text = "CAMERA 2";
                tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
            }
        }
        Crop_Image crop;
        private void btnImage_Click(object sender, EventArgs e)
        {
            if(crop != null)
            {
                crop.Dispose();
                crop = null;
            }

            crop= new Crop_Image(this);
            crop.Show(this);
        }

        private void btnSelectQR_Click(object sender, EventArgs e)
        {

            if (crop != null)
            {
                crop.Dispose();
                crop = null;
            }

            crop = new Crop_Image(this,true);
            crop.Show(this);
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }
    }
}
