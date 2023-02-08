using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M2_V2._00.FormComponents
{
    public partial class Options_2 : Form
    {
        public Options_2()
        {
            InitializeComponent();
        }

        private void Options_2_Load(object sender, EventArgs e)
        {
  
            // Add Event 
            btCam_1.CheckedChanged += new EventHandler(btCam_CheckedChanged);
            btCam_2.CheckedChanged += new EventHandler(btCam_CheckedChanged);
            foreach (ToolStripItem item in statusStrip.Items)
            {
                item.Text = "";
            }
            btCam_1.Checked = true;
        }

        private int current_type = -1;
        private void btCam_CheckedChanged(object sender, EventArgs e)
        {
            var button =  (RadioButton)sender;
            if(button.Checked)
            {
                current_type = getType();
                toolStripStatusType.Text = "Type : " + current_type.ToString();
                //if (button.Name == "btCam_1")
                //{
                //    // 
                //}else if(button.Name == "btCam_2")
                //{
                //    //Console.WriteLine("Cam 02");
                //}
                loadListImage();
            }
        }

        private int getType()
        {
            if(btCam_1.Checked)
            {
                return 0;
            }if(btCam_2.Checked)
            {
                return 1;
            }
            return -1;
        }

        private async void loadListImage()
        {
            // flowLayoutPanel clear
            flowLayoutPanel.Controls.Clear();
            // load list image
            var list = Modules.Setting.GetListImage(current_type);
            foreach (var item in list)
            {
                var image = new PictureBox();
                image.Image = Image.FromFile(item.path_image);
                image.SizeMode = PictureBoxSizeMode.Zoom;
                image.Width = 100;
                image.Height = 100;
                image.Margin = new Padding(5);
                image.AccessibleName = item.id.ToString();
                image.MouseClick += new MouseEventHandler(image_Click);
                flowLayoutPanel.Controls.Add(image);
                await Task.Delay(10);
            }
        }
        private int image_id = -1;
        private void image_Click(object sender, MouseEventArgs e)
        {
            var img = (PictureBox)sender;
            image_id = int.Parse(img.AccessibleName);
            toolStripStatusImageId.Text = " imageid :"+image_id.ToString();
        }
    }
}
