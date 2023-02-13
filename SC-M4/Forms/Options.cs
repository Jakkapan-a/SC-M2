using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M4.Forms
{
    public partial class Options : Form
    {
        public Home home;
        public Options()
        {
            InitializeComponent();
        }
        public Options(Home home)
        {
            InitializeComponent();
            this.home = home;
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

        public async void loadListImage()
        {
            // flowLayoutPanel clear
            flowLayoutPanel.Controls.Clear();
            // load list image
            var list = Modules.Setting.GetListImage(current_type);
            Console.WriteLine(list.Count());
            foreach (var item in list)
            {
                var image = new PictureBox();

                image.Image = Image.FromFile(item.path_image);

                image.SizeMode = PictureBoxSizeMode.Zoom;
                image.Width = 250;
                image.Height = 100;
                image.Margin = new Padding(5);
                image.AccessibleName = item.id.ToString();
                image.MouseDown += new MouseEventHandler(image_Click);
                flowLayoutPanel.Controls.Add(image);
                await Task.Delay(10);
            }
        }
        private int image_id = -1;
        private void image_Click(object sender, MouseEventArgs e)
        {
            // Border color
            foreach (PictureBox item in flowLayoutPanel.Controls)
            {
                item.BorderStyle = BorderStyle.None;
            }
            var img = (PictureBox)sender;
            image_id = int.Parse(img.AccessibleName);
            toolStripStatusImageId.Text = " imageid :"+image_id.ToString();
            // Border color
            img.BorderStyle = BorderStyle.FixedSingle;
        }
        Crop_Image crop;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(crop != null)
            {
                crop.Close();
                crop.Dispose();
            }

            if(btCam_1.Checked)
            {
                crop = new Crop_Image(this.home, this, 0);
            }
            else if(btCam_2.Checked)
            {
                crop = new Crop_Image(this.home, this, 1);
            }
            crop.Show(this);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Modules.Setting.RemoveBinding(image_id);
                loadListImage();
            }
        }
        Percent percent;
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(percent != null)
            {
                percent.Close();
                percent.Dispose();
            }

            percent = new Percent(image_id);
            percent.Show(this);
        }
    }
}
