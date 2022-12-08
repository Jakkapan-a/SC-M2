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
using System.Text.RegularExpressions;

namespace SC_M2
{
    public partial class Edit : Form
    {
        Modules.Model model;
        Modules.ImageList Images = new Modules.ImageList();

        Setteing setteing;
        private VideoCapture capture;
        private bool IsCapture;
        Rectangle Rect;
        System.Drawing.Point LocationXY;
        System.Drawing.Point LocationX1Y1;
        bool IsMouseDown = false;

        private string _path = @"./system";
        public Edit(int id,Setteing setteing)
        {
            InitializeComponent();
            this.model = new Model(id);
            renderPicture();
            toolStripStatusLabel_ImageID.Text = "";
            this.setteing = setteing;
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            tbName.Text = model.fullname;
            tbAccept.Value = model.percent;
            
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            foreach (var device in videoDevices)
            {
                comboBoxDevice.Items.Add(device.Name);
            }
            comboBoxDevice.SelectedIndex = 0;
            if(!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbName.Text == String.Empty)
                {
                    throw new Exception("Please enter name");
                }
                this.model.fullname = tbName.Text.Trim();
                this.model.name = tbName.Text.Trim().Substring(0, tbName.Text.Trim().Length - 10);
                this.model.percent = Convert.ToInt32(tbAccept.Value);

                // Update
                this.model.Update();
                MessageBox.Show("Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            int deviceIndex = comboBoxDevice.SelectedIndex;
            capture = new VideoCapture(deviceIndex);
            capture.Open(deviceIndex);
            SetSizeImage();
            timerVideo.Start();
            IsCapture = true;
        }

        private void SetSizeImage(string name = "HD")
        {
            int _height = 1080;
            int _width = 1920;
            switch (name.ToUpper())
            {
                case "HD":
                    _height = 720;
                    _width = 1280;
                    break;
                case "FULLHD":
                    _height = 1080;
                    _width = 1920;
                    break;
            }

            capture.Set(VideoCaptureProperties.FrameHeight, _height);
            capture.Set(VideoCaptureProperties.FrameWidth, _width);
        }
        private void Edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeCaptureResources();
            timerVideo.Stop();
        }

        private void tbDisconnect_Click(object sender, EventArgs e)
        {
            DisposeCaptureResources();
            timerVideo.Stop();
        }

        private void timerVideo_Tick(object sender, EventArgs e)
        {
            if (capture.IsOpened())
            {
                try
                {
                    using (Mat frame = new Mat())
                    {
                        capture.Read(frame);
                        if (!frame.Empty())
                        {
                            pictureBoxC.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                        }
                    }
                }
                catch (Exception)
                {
                    pictureBoxC.Image = null;
                }
                finally
                {
                }
            }
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

        private void pictureBoxC_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            LocationXY = e.Location;
        }

        private void pictureBoxC_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                LocationX1Y1 = e.Location;
                Refresh();
            }
        }

        private void pictureBoxC_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                LocationX1Y1 = e.Location;
                Refresh();
                IsMouseDown = false;
            }  
        }

        private void pictureBoxC_Paint(object sender, PaintEventArgs e)
        {
            if(Rect != null&& IsCapture)
            {
                e.Graphics.DrawRectangle(Pens.Red, GetRect());
            }
        }
        private Rectangle GetRect()
        {
            Rect = new Rectangle();
            Rect.X = Math.Min(LocationXY.X, LocationX1Y1.X);
            Rect.Y = Math.Min(LocationXY.Y, LocationX1Y1.Y);
            Rect.Width = Math.Abs(LocationXY.X - LocationX1Y1.X);
            Rect.Height = Math.Abs(LocationXY.Y - LocationX1Y1.Y);
            return Rect;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                using (Bitmap bitmap = new Bitmap(pictureBoxC.Image))
                {
                    using (Bitmap bmp = new Bitmap(Rect.Width, Rect.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.DrawImage(bitmap, 0, 0, Rect, GraphicsUnit.Pixel);
                        }
                        string filename = $"{Guid.NewGuid()}.jpg";
                        string path = $"{_path}/images/{filename}";
                        CheckPath($"{_path}/images/");
                        bmp.Save(path, ImageFormat.Jpeg);

                        // Save to database
                        Modules.ImageList image = new Modules.ImageList(filename, path, model.id);
                        image.Save();
                        MessageBox.Show("Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisposeCaptureResources();
            renderPicture();
        }

        private void renderPicture()
        {
            flowLayoutPanel.Controls.Clear();
            SC_M2.Modules.ImageList image = new SC_M2.Modules.ImageList();
            image.model_id = model.id;
            var list = image.GetModel();
            foreach(var item in list)
            {
                var pb = new PictureBox();
                pb.Height = 112;
                pb.Width = 200;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = Image.FromFile(item.path);
                pb.Tag = item.id;
                pb.BorderStyle = BorderStyle.FixedSingle;
                pb.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBox_Click);

                // Add Flow
                flowLayoutPanel.Controls.Add(pb);
            }
            flowLayoutPanel.Update();
        }

        
        private void pictureBox_Click(object sender, MouseEventArgs e)
        {
            // Picture Box
            PictureBox pb = (PictureBox)sender;
            // Get and set ID
            Images.id = (int)pb.Tag;
            toolStripStatusLabel_ImageID.Text = "Image ID: " + Images.id;

        }
        
        private void deleteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Images.Get();
            Images.Delete();
            renderPicture();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete or not
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this model and image?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                    SC_M2.Modules.ImageList image = new SC_M2.Modules.ImageList();
                    image.Delete(model.id);
                    model.Delete();
                    setteing.loadTable();
                    this.Close();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

            }
        }
    }
}
