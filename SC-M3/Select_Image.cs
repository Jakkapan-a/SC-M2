using SC_M3.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M3
{
    public partial class Select_Image : Form
    {
        private int index = -1;
        public Select_Image(int index)
        {
            InitializeComponent();
            this.index = index;
        }

        private VideoCAM videoCAM;
        private void Select_Image_Load(object sender, EventArgs e)
        {
            videoCAM = new VideoCAM();
            videoCAM.OnVideoFrameHandler += VideoCAM_OnVideoFrameHandler;
            videoCAM.Start(index);
        }
        private delegate void frameVideo(Bitmap bitmap);
        private void VideoCAM_OnVideoFrameHandler(Bitmap bitmap)
        {
            if (scrollablePictureBox1.InvokeRequired)
            {
                scrollablePictureBox1.Invoke(new frameVideo(VideoCAM_OnVideoFrameHandler),bitmap);
                return;
            }
            else
            {
                scrollablePictureBox1.Image = new Bitmap(bitmap);
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Rectangle rectangle = scrollablePictureBox1.GetRect();
            if(rectangle.Width==0 || rectangle.Height==0)
            {
                MessageBox.Show("Please select area", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Bitmap bitmap = new Bitmap(scrollablePictureBox1.Image);
            // Create a new bitmap and draw the specified area of the source bitmap to it.
            Bitmap bmp = new Bitmap(rectangle.Width, rectangle.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(bitmap, 0, 0, rectangle, GraphicsUnit.Pixel);
            }
            // Save the bitmap to a file.
            string file_name = Guid.NewGuid().ToString()+"_"+DateTime.Now.ToString("ddMMyyyyHHmmss")+".jpg";
            file_name = Path.Combine(SC_M3.Properties.Resources.path_images,file_name);
            bmp.Save(file_name,ImageFormat.Jpeg);
            // Message Box
            MessageBox.Show("Save image success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Select_Image_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(videoCAM!=null)
            {
                videoCAM.Stop();
                videoCAM.Dispose();
            }
        }
    }
}
