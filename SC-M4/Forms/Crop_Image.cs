using OpenCvSharp;
using SC_M4.Modules;
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
namespace SC_M4.Forms
{
    public partial class Crop_Image : Form
    {
        //int drive = 0;
        private Options options;
        private int _type = -1;
        private Home home;
        public Crop_Image(Home home,Options options, int _type)
        {
            InitializeComponent();

            this.options = options;
            this._type = _type;
            this.home = home;
        }

        private void timerVideo_Tick(object sender, EventArgs e)
        {
            try
            {
               if(_type == 0 && home != null && home.bitmapCamaera_01 != null)
                {
                    pictureCrop.Image = home.bitmapCamaera_01;
                }
                else if (_type == 1 && home != null && home.bitmapCamaera_02 != null)
                {
                    pictureCrop.Image = home.bitmapCamaera_02;
                }
            }
            catch (Exception ex)
            {
                this.timerVideo.Stop();
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        Rectangle Rect;
        private void Crop_Image_Load(object sender, EventArgs e)
        {
            this.timerVideo.Start();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Rect = pictureCrop.GetRect();
                using (Bitmap bitmap = new Bitmap(pictureCrop.Image))
                {
                    using (Bitmap bmp = new Bitmap(Rect.Width, Rect.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.DrawImage(bitmap, 0, 0, Rect, GraphicsUnit.Pixel);
                        }

                        string filename = $"{Guid.NewGuid()}.jpg";

                        string path = Properties.Resources.path_images;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        path= Path.Combine(path, filename);
                        bmp.Save(path, ImageFormat.Jpeg);
                        Setting setting = new Setting();
                        setting.state = 1;
                        setting.path_image = path;
                        setting._type = _type;
                        setting.Save();
                        /*
                            Re-Load image 
                            Close
                        */
                        options.loadListImage();
                        MessageBox.Show("Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Crop_Image_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
