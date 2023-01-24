using OpenCvSharp;
using SC_M2_V2._00.Modules;
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
using Set = SC_M2_V2._00.Modules.Setting;
namespace SC_M2_V2._00.FormComponents
{
    public partial class Crop_Image : Form
    {
        private OpenCvSharp.VideoCapture capture;
        //int drive = 0;
        private Options options;
        private bool state = false;
        public Crop_Image(Options options = null, bool state = false)
        {
            InitializeComponent();

            if(options != null)
            {
                this.options = options;
            }

            if(state)
            {
                this.state = state;
            }
        }

        private void timerVideo_Tick(object sender, EventArgs e)
        {
            try
            {
                if (capture != null && capture.IsOpened())
                {
                    using (OpenCvSharp.Mat frame = new OpenCvSharp.Mat())
                    {
                        capture.Read(frame);
                        if (frame != null)
                        {
                            pictureCrop.SuspendLayout();
                            pictureCrop.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                            pictureCrop.ResumeLayout();
                        }
                    }
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
            //SC_M2_V2._00.Properties.Resources.Path_Image
            if (!Directory.Exists(SC_M2_V2._00.Properties.Resources.Path_Image))
            {
                Directory.CreateDirectory(SC_M2_V2._00.Properties.Resources.Path_Image);
            }

            if (!state)
            {
                capture = new OpenCvSharp.VideoCapture(this.options.comboBoxCamera.SelectedIndex);
                capture.Open(this.options.comboBoxCamera.SelectedIndex);
                capture.FrameHeight = int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight);
                capture.FrameWidth = int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth);
                timerVideo.Start();
            }
            else
            {
                var setting = Set.GetSetting(1); // Cam 2
                if(setting[0].path_image != "")
                {
                    pictureCrop.Image = Image.FromFile(setting[0].path_image);
                }
                else { pictureCrop.Image = null; }
                
            }
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
                        string path = $"{SC_M2_V2._00.Properties.Resources.Path_Image}/{filename}";
                        bmp.Save(path, ImageFormat.Jpeg);
                        int _type = 0 ;
                        if(!state)
                        {
                            if(this.options.listBoxItem.SelectedIndex == 0)
                            {
                                _type = 0;
                            }
                            else
                            {
                                _type = 1;
                            }
                        }
                        else
                        {
                            _type = 2;
                        }
                        var setting = Set.GetSetting(_type);
                        setting[0].state = 0;
                        setting[0].Update();

                        var set = new Set();
                        set.state = 1;
                        set.path_image= path;
                        set._type = _type;
                        set.Save();

                        if(_type != 2)
                        {
                            this.options.pictureBox1.Image = Image.FromFile(path);
                        }
                        else
                        {
                            this.options.pictureBox2.Image = Image.FromFile(path);
                        }

                        MessageBox.Show("Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (capture != null && capture.IsOpened())
                        {
                            capture.Release();
                            capture.Dispose();
                            capture = null;
                        }
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
            if (capture != null && capture.IsOpened())
            {
                capture.Release();
                capture.Dispose();
                capture= null;
            }
        }
    }
}
