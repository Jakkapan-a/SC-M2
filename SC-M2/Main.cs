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
//using OpenCvSharp;
//using OpenCvSharp.Extensions;
using System.IO;
using SC_M2.Modules;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SC_M2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };

        //private bool isCameraOpen = false;
        //private bool isConnection = false;

        private string _path = @"./system";

        int Count = 0;
        private OpenCvSharp.VideoCapture capture;
        private bool IsCapture;
        
        private void Main_Load(object sender, EventArgs e)
        {
            reloadTable();
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            foreach (var device in videoDevices)
            {
                comboBoxCameraDevice.Items.Add(device.Name);
            }
            if(comboBoxCameraDevice.Items.Count >0 )
                comboBoxCameraDevice.SelectedIndex = 0;

            comboBoxBaudRate.Items.AddRange(baudList);
            if (comboBoxBaudRate.Items.Count > 0)
                comboBoxBaudRate.SelectedIndex = baudList.Length -1;

            comboBoxComPort.Items.AddRange(SerialPort.GetPortNames());

            if (comboBoxComPort.Items.Count > 0)
                comboBoxComPort.SelectedIndex = 0;

            tbName.Select();
            this.ActiveControl = tbName;
            tbName.Focus();

            toolStripStatusLabelConnectControl.Text = "Not Connected";
            string log = "/temp";
            //string[] files = Directory.GetFiles(_path + log);
            DirectoryInfo yourRootDir = new DirectoryInfo(_path + log);
            if (!Directory.Exists(_path + log))
            {
                Directory.CreateDirectory(_path + log);
            }
            
            try
            {
                foreach (FileInfo file in yourRootDir.GetFiles())
                {
                    if (file.LastWriteTime < DateTime.Now.AddDays(-1))
                    { 
                        file.Delete(); 
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            timerCouter.Start();
            toolStripStatusLabelConnectControl.Text = "";
            toolStripStatusData.Text = "Ready";
            toolStripStatusLabelData.Text = "";

            try
            {
                var delete = Delete_image.GetAll();
                foreach (var item in delete)
                {
                    if (File.Exists(item.path))
                    {
                        File.Delete(item.path);
                        item.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

        }


        private void _KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string name = (sender as TextBox).Name;
                    switch (name)
                    {
                        case "tbName":
                            tbQrcode.Select();
                            this.ActiveControl = tbQrcode;
                            tbQrcode.Focus();
                            break;
                        case "tbQrcode":
                            if (pictureBoxC.Image == null || capture == null)
                            {
                                btConnect.PerformClick();
                            }
                            Processing();
                            tbQrcode.Select();
                            this.ActiveControl = tbQrcode;
                            tbQrcode.Focus();
                            if (!timerCouter.Enabled)
                            {
                                timerCouter.Start();
                            }
                            break;
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
        Modules.Model model = new Modules.Model();
        private async void Processing()
        {
            SetOutput("L");
            Count = 0;
            await Task.Delay(100);
            using (History history = new History())
            {
                history.name = tbName.Text;
                history.qrcode = tbQrcode.Text;
                string name = tbQrcode.Text.Trim().Substring(0, tbQrcode.Text.Trim().Length - 10);
                var list = Model.GetByName(name);
                history.model = name;
                if (list.Count > 0)
                {

                    if (ImageProcessing(list[0].id)){
                        //Console.WriteLine("true");
                        history.judgement = "OK";
                        SetOutput("OK");
                        sendSerialData("OK");
                    }
                    else
                    {
                        //Console.WriteLine("false");
                        history.judgement = "NG";
                        SetOutput("NG");
                    }
                }
                else
                {
                    history.judgement = "NG";
                    SetOutput("NG");
                }
                history.Save();
                reloadTable();
                tbQrcode.Text = "";
            }
        }


        private bool ImageProcessing(int model_id)
        {

            if (pictureBoxC.Image == null || capture == null)
            {
                throw new Exception("Please connection to camera");
            }
            this.model.id = model_id;
            this.model.GetRow();

            var list = Modules.ImageList.GetModel(model_id);
            if (list.Count == 0 )
                throw new Exception("Not found image in model");


            
            string log = "/temp";
            using (Bitmap bmpC = new Bitmap(pictureBoxC.Image))
            {
                foreach (var im in list)
                {
                    using (Bitmap bm = new Bitmap(matching(new Bitmap(im.path), bmpC)))
                    {
                       
                        if (!Directory.Exists(_path+log))
                        {
                            Directory.CreateDirectory(_path + log);
                        }
                        // 1
                        string image1 = "/A-" + Guid.NewGuid().ToString() + ".jpg";
                        string path_bm = _path + log + image1;
                        var bmm = new Image<Gray, byte>(im.path);
                        bmm.Save(path_bm);
                        // 2
                        string image2 = "/B-" + Guid.NewGuid().ToString() + ".jpg";
                        string bml = _path + log+ image2;
                        bm.Save(bml);
                        var bmll = new Image<Gray, byte>(bml);

                        
                        double compare = Compare(bmm, bmll);
                        Console.WriteLine(compare.ToString()+"%");


                        string logpath = _path+"/log/"+ DateTime.Now.ToString("yyyy-MM-dd_HH")+"-Log.txt";

                        if (!Directory.Exists(_path + "/log/"))
                            Directory.CreateDirectory(_path + "/log/");

                        if (!File.Exists(logpath))
                            File.Create(logpath).Dispose();
                        
                        // Add text log
                        using (StreamWriter sw = File.AppendText(logpath))
                        {
                            sw.WriteLine("<--------------------------------------------->");
                            sw.WriteLine("Time : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            sw.WriteLine("Image 1 : " + image1);
                            sw.WriteLine("Image 2 : " + image2);
                            sw.WriteLine("Result : " + compare +"%");
                        }
                        
                        if (compare < model.percent)
                        {
                            return false;
                        }
                        bmm.Dispose();
                        bmll.Dispose();
              
                    }
                }
            }
           
            return true;
        }

        public double Compare(Image<Gray, byte> imageMaster, Image<Gray, byte> imageSlave)
        {
            try
            {
                if (imageMaster.Width != imageSlave.Width || imageMaster.Height != imageSlave.Height)
                {
                    return 0;
                }
                var diffImage = new Image<Gray, byte>(imageMaster.Width, imageMaster.Height);
                // Get the image of different pixels.
                CvInvoke.AbsDiff(imageMaster, imageSlave, diffImage);
                var threadholdImage = new Image<Gray, byte>(imageMaster.Width, imageMaster.Height);
                // Check the pixies difference.
                // For instance, if difference between the same pixel on both image are less then 20,
                // we can say that this pixel is the same on both images.
                // After threadholding we would have matrix on which we would have 0 for pixels which are "nearly" the same and 1 for pixes which are different.
                CvInvoke.Threshold(diffImage, threadholdImage, 20, 1, Emgu.CV.CvEnum.ThresholdType.Binary);
                int diff = CvInvoke.CountNonZero(threadholdImage);
                // Take the percentage of the pixels which are different.
                var deffPrecentage = diff / (double)(imageMaster.Width * imageMaster.Height);
                // If the amount of different pixeles more then 15% then we can say that those immages are different.
                var percent = deffPrecentage * 100;
                // round off
                return Math.Round(100 - percent, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("E006" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

        }

        private Bitmap matching(Bitmap imageMaster, Bitmap imageSlave, string pathSave = null)
        {
            try
            {
                var imgScene = imageSlave.ToImage<Bgr, byte>();// new Bitmap(@"D:\CPush\Image\008.JPG").ToImage<Bgr, byte>(); // Image imput
                var template = imageMaster.ToImage<Bgr, byte>(); // new Bitmap(@"D:\CPush\Image\007.JPG").ToImage<Bgr, byte>(); // Master 
                string pathCurrent = Directory.GetCurrentDirectory();

                Mat imgout = new Mat();

                CvInvoke.MatchTemplate(imgScene, template, imgout, Emgu.CV.CvEnum.TemplateMatchingType.CcorrNormed);

                double minVal = 0.0;
                double maxVal = 0.0;
                Point minLoc = new System.Drawing.Point();
                Point maxLoc = new Point();

                CvInvoke.MinMaxLoc(imgout, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
                Rectangle r = new Rectangle(maxLoc, template.Size);
                var imgCrop = imgScene.Copy(r);
                CvInvoke.Rectangle(imgScene, r, new MCvScalar(0, 0, 255), 2);
                //this.pictureBox2.Image = imgScene.ToBitmap();

                if (pathSave != null)
                {
                    imgScene.Save(pathSave);
                }
                //this.pictureBox1.Hide();
                return imgCrop.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("E007 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            int deviceIndex = comboBoxCameraDevice.SelectedIndex;
            capture = new OpenCvSharp.VideoCapture(deviceIndex);
            capture.Open(deviceIndex);
            SetSizeImage();
            
            timerVideo.Start();
            IsCapture = true;

            tbQrcode.Select();
            this.ActiveControl = tbQrcode;
            tbQrcode.Focus();

            // Serial
            ConnectionSerial();
            
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

            capture.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, _height);
            capture.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, _width);
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
                pictureBoxC.SizeMode = PictureBoxSizeMode.Zoom;
            }

            if (timerCouter.Enabled)
            {
                timerCouter.Stop();
                Count = 0;
            }

        }

        private void timerVideo_Tick(object sender, EventArgs e)
        {

            if (capture.IsOpened() && capture != null)
            {
                try
                {
                    using (OpenCvSharp.Mat frame = new OpenCvSharp.Mat())
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

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeCaptureResources();
            Setteing st = new Setteing();
            st.ShowDialog(this);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeCaptureResources();
        }
        private void SetOutput(string input="wait")
        {
            switch (input.ToUpper())
            {
                case "OK":
                    lbOutput.Text = "OK";
                    lbOutput.BackColor = Color.Green;
                    break;
                case "NG":
                    lbOutput.Text = "NG";
                    lbOutput.BackColor = Color.Red;
                    break;
                case "L":
                    lbOutput.Text = "Loading..";
                    lbOutput.BackColor = Color.Yellow;
                    break;
                case "WAIT":
                    lbOutput.Text = "Wait..";
                    lbOutput.BackColor = Color.Transparent;
                    break;
            }
        }
        private void reloadTable()
        {
            try
            {
                dataGridView1.DataSource = null;
                var table = History.GetAll();
                int num = 1;
                var ml2 = (from x in table
                           select new
                           {
                               ID = x.id,
                               No = num++,
                               Name = x.name,
                               Model = x.model,
                               Qr_Code = x.qrcode,
                               Juggement = x.judgement,
                               x.created_at
                           }).ToList();
                
                dataGridView1.DataSource = ml2;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = (int)(dataGridView1.Width * 0.1);
                dataGridView1.Refresh();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }
        
        private void timerCouter_Tick(object sender, EventArgs e)
        {
            Count++;
            if (Count > 60)
            {
                Count = 0;
            }

            if (!serialPort.IsOpen)
            {
                toolStripStatusLabelConnectControl.Text = "Disconnection";
                toolStripStatusLabelConnectControl.ForeColor = Color.Red;
            }
        }

        public bool ConnectionSerial()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            try
            {
                serialPort.PortName = comboBoxComPort.SelectedItem.ToString();
                serialPort.BaudRate = Int32.Parse(comboBoxBaudRate.SelectedItem.ToString());
                serialPort.Open();
                toolStripStatusLabelConnectControl.Text = "Connection: " + comboBoxComPort.SelectedItem.ToString() + " " + comboBoxBaudRate.SelectedItem.ToString();
                toolStripStatusLabelConnectControl.ForeColor = Color.Green;
                sendSerialData("Conn");
            }
            catch(Exception ex)
            {
                MessageBox.Show("E008 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return serialPort.IsOpen;
        }

        public void sendSerialData(string data)
        {
            try
            {
                toolStripStatusData.Text = String.Empty;
                toolStripStatusData.Text = "Data: " + data;
                if (serialPort.IsOpen)
                {
                    serialPort.Write(">" + data.ToUpper() + "<#");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void connectControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionSerial();
        }

        private void connectCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btConnect.PerformClick();
        }
    }
}
