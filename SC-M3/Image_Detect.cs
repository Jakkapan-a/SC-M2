using DirectShowLib;
using SC_M3.Modules;
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
    public partial class Image_Detect : Form
    {
        public Image_Detect()
        {
            InitializeComponent();
        }

        private void Image_Detect_Load(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            foreach (var device in videoDevices)
            {
                comboBox1.Items.Add(device.Name);
            }
            if(comboBox1.Items.Count>0)
                comboBox1.SelectedIndex = 0;
        }
        private Select_Image _select;
        private void btSelect_Click(object sender, EventArgs e)
        {
            if (_select != null)
            {
                _select.Close();
                _select= null;
            }
            if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select camera", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _select = new Select_Image(comboBox1.SelectedIndex);
            _select.ShowDialog();
        }

        private void load_image()
        {
            var _image = Master_image.LoadLastImage();
            if(_image != null)
            {

            }
        }
    }
}
