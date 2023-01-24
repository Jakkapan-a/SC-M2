﻿using OpenCvSharp;
using SC_M2_V2_00.FormComponent;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SC_M2_V2._00.Ocr;
using System.IO;
using System.Drawing.Imaging;
using SC_M2_V2._00.Properties;
using SC_M2_V2._00;
using SC_M2_V2._00.FormComponents;
using Emgu.CV;
using Emgu.CV.Structure;
using SC_M2_V2._00.Modules;
using System.Windows.Markup;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace SC_M2_V2_00
{
    public partial class Home : Form
    {
        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };
        public int deviceCamera1 = -1;
        public int deviceCamera2 = -1;

        protected string curLangCode = "eng";
        protected IList<Image> imageList;
        protected string inputfilename;
        protected int imageIndex;
        protected float scaleX = 1f;
        protected float scaleY = 1f;
        protected string selectedPSM = "Auto"; // 3 - Fully automatic page segmentation, but no OSD (default)
        protected string selectedOEM = "3"; // Default
        private string inputfilenameimage;
        private string inputfilenameimage2;
        public string serialportName = string.Empty;
        public string baudrate = string.Empty;

        public string dataSerialReceived = string.Empty;
        public string readDataSerial = string.Empty;

        private OpenCvSharp.VideoCapture videoCapture1;
        private OpenCvSharp.VideoCapture videoCapture2;

        private bool isVideoCapture1 = false, isVideoCapture2 = false;
        //public Dictionary<string, bool> statusCamConnect = new Dictionary<string, bool>();

        List<Image> ListImage = new List<Image>();

        public bool isVideoCapture = false;
        private bool isStaetReset = true;

        private int countDetect = 0;

        private Process _processCMD;
        private StreamWriter _streamWriterCMD;

        public string _path_defult;
        private int _stepImageClassification = 0;
        private string LabelSW;

        private bool is_Blink_NG = false;
        public Home()
        {
            InitializeComponent();
        }
        SC_M2_V2_00.FormComponent.Connections connections;

        private void Home_Load(object sender, EventArgs e)
        {
            //statusCamConnect.Clear();
            //statusCamConnect.Add("connect", true);
            //statusCamConnect["connect"] = false;
            timerMain.Start();

            _path_defult = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(_path_defult);
            if (!Directory.Exists(SC_M2_V2._00.Properties.Resources.Path_System_Temp))
            {
                Directory.CreateDirectory(SC_M2_V2._00.Properties.Resources.Path_System_Temp);
            }

            if (!Directory.Exists(SC_M2_V2._00.Properties.Resources.Path_ImageClassification))
            {
                Directory.CreateDirectory(SC_M2_V2._00.Properties.Resources.Path_ImageClassification);
            }

            Process _processCMD = new Process();
            _processCMD.StartInfo.WorkingDirectory = SC_M2_V2._00.Properties.Resources.Path_ImageClassificationTemp;
            _processCMD.StartInfo.FileName = "cmd.exe";
            _processCMD.StartInfo.UseShellExecute = false;
            _processCMD.StartInfo.RedirectStandardOutput = true;
            _processCMD.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            _processCMD.StartInfo.RedirectStandardError = true;
            _processCMD.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;
            _processCMD.StartInfo.RedirectStandardInput = true;
            _processCMD.StartInfo.CreateNoWindow = false;
            _processCMD.EnableRaisingEvents = true;
            _processCMD.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _processCMD.Start();
            _processCMD.BeginOutputReadLine();
            _streamWriterCMD = _processCMD.StandardInput;
            _streamWriterCMD.AutoFlush = true;
            _streamWriterCMD.WriteLine("ImageClassification.exe -file");
            _processCMD.OutputDataReceived += new DataReceivedEventHandler(this.process_OutputReceived);

            // Clear statusStripStatus text
            foreach (ToolStripItem item in statusStripStatus.Items)
            {
                item.Text = "";
            }
            lbTitle.Text = "Please open the camera";
            btnOCR.Visible = false;
            btnOCR2.Visible = false;

            // Focus on the first txtEmployee
            this.ActiveControl = txtEmployee;
            txtEmployee.Focus();
            loadTableHistory();
        }

        private void process_OutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            Console.WriteLine("Data : " + e.Data);
            if (_stepImageClassification == 1)
            {
                string data = e.Data.ToString();
                if (data != "NotFound")
                {
                    var array = data.Split('-');
                    //Console.WriteLine(data);
                    var label = array[0];
                    this.LabelSW = label.Split(':')[1];
                }
                _stepImageClassification = 2;
            }
        }

        private void conectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connections != null)
            {
                connections.Close();
                connections.Dispose();
                connections = null;
            }
            connections = new SC_M2_V2_00.FormComponent.Connections(this);
            connections.Show();
        }

        #region Serial Port 
        public void setSerialPort(string portName, string baud)
        {
            this.serialportName = portName;
            this.baudrate = baud;
        }
        private void serialConnect(string portName, int baud)
        {
            try
            {
                if (this.serialPort.IsOpen)
                {
                    this.serialPort.Close();
                }

                this.serialPort.PortName = portName;
                this.serialPort.BaudRate = baud;
                this.serialPort.Open();
                this.serialCommand("conn#");
                Thread.Sleep(100);
                // this.serialCommand("OK#");
                this.toolStripStatusConnectSerialPort.Text = "Serial Port: Connected";
                this.toolStripStatusConnectSerialPort.ForeColor = Color.Green;

            }
            catch (Exception ex)
            {
                this.toolStripStatusConnectSerialPort.Text = "Serial Port: Disconnect";
                this.toolStripStatusConnectSerialPort.ForeColor = Color.Red;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void serialConnect()
        {

            if (this.serialportName == string.Empty || this.baudrate == string.Empty)
            {
                MessageBox.Show("Please select serial port and baud rate", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.serialConnect(this.serialportName, int.Parse(this.baudrate));
        }

        public void serialCommand(string command)
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Write(">" + command + "<#");
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                readDataSerial = this.serialPort.ReadExisting();
                this.Invoke(new EventHandler(dataReceived));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataReceived(object sender, EventArgs e)
        {
            this.dataSerialReceived += readDataSerial;
            if (dataSerialReceived.Contains(">") && dataSerialReceived.Contains("<"))
            {
                string data = this.dataSerialReceived.Replace("\r", string.Empty).Replace("\n", string.Empty);
                data = data.Substring(data.IndexOf(">") + 1, data.IndexOf("<") - 1);
                this.dataSerialReceived = string.Empty;
                Console.WriteLine("RST : "+data);
                // Console.WriteLine("Received ->: " + data);
                if (data == "rst")
                {
                  
                    isStaetReset = true;
                    is_Blink_NG = false;
                    lbTitle.Text = "Waiting for detection....";
                    lbTitle.ForeColor = Color.Black;
                    lbTitle.BackColor = Color.Orange;
                    countDetect = 0;

                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    pictureBoxCamDetect1.Image = null; pictureBoxCamDetect2.Image = null;

                }
            }
            else if (!dataSerialReceived.Contains(">"))
            {
                this.dataSerialReceived = string.Empty;
            }
        }

        #endregion

        private void setCameraDevice(int device1, int device2)
        {
            if (device1 == -1 || device2 == -1)
            {
                MessageBox.Show("Please select camera device", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.deviceCamera1 = device1;
            this.deviceCamera2 = device2;
        }


        private async void cameraConnect()
        {
            try
            {
                if(txtEmployee.Text == string.Empty)
                {
                    MessageBox.Show("Please input employee ID", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Console.WriteLine("Start " + DateTime.Now.ToString("HH:mm:ss"));
                if (this.videoCapture1 != null)
                {
                    this.videoCapture1.Dispose();
                    this.videoCapture1 = null;
                }
                if (this.videoCapture2 != null)
                {
                    this.videoCapture2.Dispose();
                    this.videoCapture2 = null;
                }

                if (this.deviceCamera1 == -1 || this.deviceCamera2 == -1)
                {
                    MessageBox.Show("Please select driver of camera!", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                lbTitle.Text = "Connecting...";
                this.timerVideo1.Stop();
                this.timerVideo2.Stop();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                this.videoCapture1 = new OpenCvSharp.VideoCapture(deviceCamera1);
                this.videoCapture1.Open(deviceCamera1);
                this.videoCapture1.FrameHeight = 720;
                this.videoCapture1.FrameWidth = 1280;
                await Task.Delay(1000);
                this.videoCapture2 = new OpenCvSharp.VideoCapture(deviceCamera2);
                this.videoCapture2.Open(deviceCamera2);
                this.videoCapture2.FrameHeight = 720;
                this.videoCapture2.FrameWidth = 1280;
                _stepImageClassification = 0;

                this.timerVideo1.Start();
                //this.timerVideo2.Start();
                stopwatch.Stop();
                countDetect = 0;
                Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

                lbTitle.Text = "Waiting for detection....";
                lbTitle.ForeColor = Color.Black;
                lbTitle.BackColor = Color.Yellow;
                toolStripStatusConnectionCamera.Text = "Camera: Connected";
                toolStripStatusConnectionCamera.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void setVideoCapture(OpenCvSharp.VideoCapture capture, int FrameWidth, int FrameHeight)
        {
            capture.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, FrameWidth);
            capture.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, FrameHeight);
        }

        private void disposeCamera()
        {
            try
            {
                if (this.videoCapture1 != null)
                {
                    if (this.videoCapture1.IsOpened())
                    {
                        this.videoCapture1.Release();
                    }
                    this.timerVideo1.Stop();
                    this.videoCapture1.Dispose();
                    this.videoCapture1 = null;
                }
                if (this.videoCapture2 != null)
                {
                    if (this.videoCapture2.IsOpened())
                    {
                        this.videoCapture2.Release();
                    }
                    // this.timerVideo2.Stop();
                    this.videoCapture2.Dispose();
                    this.videoCapture2 = null;
                }
                this.toolStripStatusConnectionCamera.Text = $"Camera: Disconnected";
                this.toolStripStatusConnectionCamera.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool toggle_blink_ng = false;
        private void timerMain_Tick(object sender, EventArgs e)
        {
            countDetect++;
            if (countDetect > 10000)
            { countDetect = 0; }
            //Console.WriteLine("Count : "+countDetect.ToString() +", Step : "+ _stepImageClassification.ToString());
            if(is_Blink_NG)
            {
               toggle_blink_ng = !toggle_blink_ng;
               
               if(toggle_blink_ng)
               {
                    lbTitle.BackColor = Color.Red;
                    lbTitle.ForeColor = Color.White;
                }
               else
               {
                    lbTitle.BackColor = Color.White;
                    lbTitle.ForeColor = Color.Red;
               }
            }else if (lbTitle.BackColor != Color.Yellow && isStaetReset)
             {
                lbTitle.BackColor = Color.Yellow;
                lbTitle.ForeColor = Color.Black;
            }
        }
        string _pathFile, _nameTemp;
        private void timerVideo1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (videoCapture1 != null && videoCapture1.IsOpened())
                {
                    using (OpenCvSharp.Mat frame = videoCapture1.RetrieveMat())
                    {
                        //frame = videoCapture1.RetrieveMat();
                        if (frame != null)
                        {
                            pictureBoxCamera1.SuspendLayout();
                            pictureBoxCamera1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                            pictureBoxCamera1.ResumeLayout();
                            if (countDetect > 3 && isStaetReset)
                            {

                                if (_stepImageClassification == 0)
                                {
                                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                                    _nameTemp = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + fileName;
                                    pictureBoxCamera1.Image.Save(_nameTemp, ImageFormat.Jpeg);
                                    _pathFile = System.IO.Path.Combine(_path_defult, "System", "temp", fileName);

                                    _streamWriterCMD.WriteLine(_pathFile);

                                    _stepImageClassification = 1;
                                }
                                else if (_stepImageClassification == 1)
                                {
                                    // Wait received
                                    LabelSW = string.Empty;
                                }
                                else if (_stepImageClassification == 2 && LabelSW != string.Empty)
                                {
                                    // Check Label SW Page
                                    Console.WriteLine("Label :" + LabelSW);
                                    if (LabelSW == "VER")
                                    {
                                        lbTitle.Text = "OCR Read Data...";
                                        // OCR 
                                        //inputfilenameimage = _nameTemp;
                                        //btnOCR.PerformClick();
                                        FuncOCR_();
                                        isStaetReset = false; // Wait Reset
                                    }
                                    else
                                    {

                                    }

                                    countDetect = 0;
                                    _stepImageClassification = 0;

                                    if (File.Exists(_pathFile))
                                    {
                                        File.Delete(_pathFile);
                                    }
                                }
                            }
                        }
                    }
                }

                if (videoCapture2 != null && videoCapture2.IsOpened())
                {
                    using (OpenCvSharp.Mat frame = new OpenCvSharp.Mat())
                    {
                        videoCapture2.Read(frame);
                        if (frame != null)
                        {
                            pictureBoxCamera2.SuspendLayout();
                            pictureBoxCamera2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                            pictureBoxCamera2.ResumeLayout();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.timerVideo1.Stop();
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message, "Exclamation B00", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void timerVideo2_Tick(object sender, EventArgs e)
        {
            try
            {
                ////Console.WriteLine("Run..");
                //if (videoCapture2 != null && videoCapture2.IsOpened())
                //{
                //    using (OpenCvSharp.Mat frame = new OpenCvSharp.Mat())
                //    {
                //        videoCapture2.Read(frame);
                //        if (frame != null)
                //        {
                //            pictureBoxCamera2.SuspendLayout();
                //            pictureBoxCamera2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                //            pictureBoxCamera2.ResumeLayout();
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                this.timerVideo2.Stop();
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.disposeCamera();
            _streamWriterCMD.WriteLine("exit");
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "Camera are opening.";
            this.cameraConnect();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.disposeCamera();
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }


        private Options options = new Options();
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (options != null)
            {
                options.Dispose();
                options = null;
            }
            options = new Options();
            options.Show(this);
        }

        private string performOCR(IList<Image> imageList, string inputfilename, int index, Rectangle rect)
        {
            try
            {
                if (curLangCode.Trim().Length == 0)
                {
                    MessageBox.Show(this, "curLangCode = 0");
                    return "";
                }
                OCRImageEntity entity = new OCRImageEntity(imageList, inputfilename, index, rect, curLangCode);
                //entity.ScreenshotMode = this.screenshotModeToolStripMenuItem.Checked;
                entity.ScreenshotMode = false;
                entity.Language = "eng";
                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PageSegMode = selectedPSM;
                ocrEngine.OcrEngineMode = selectedOEM;
                ocrEngine.Language = entity.Language;

                IList<Image> images = entity.ClonedImages;

                string result = ocrEngine.RecognizeText(((List<Image>)images).GetRange(0, 1), entity.Inputfilename);
                //this.richTextBox1.AppendText(result);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclamation A00", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return "";
        }
        List<SC_M2_V2._00.Modules.Setting> settings1 = new List<SC_M2_V2._00.Modules.Setting>();
        List<SC_M2_V2._00.Modules.Setting> settings2 = new List<SC_M2_V2._00.Modules.Setting>();

        private void backgroundWorkerOpenCamera_DoWork(object sender, DoWorkEventArgs e)
        {
            cameraConnect();
            toolStripStatusConnectionCamera.Text = SC_M2_V2._00.Properties.Resources.CamConnecting;
            toolStripStatusConnectionCamera.ForeColor = Color.Green;
        }

        private void backgroundWorkerOpenCamera_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorkerOpenCamera_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

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

        private Bitmap Matching(Bitmap imageMaster, Bitmap imageSlave, string pathSave = null)
        {
            try
            {
                var imgScene = imageSlave.ToImage<Bgr, byte>();     //  Image imput
                var template = imageMaster.ToImage<Bgr, byte>();    // Master 
                string pathCurrent = Directory.GetCurrentDirectory();

                Emgu.CV.Mat imgout = new Emgu.CV.Mat();

                CvInvoke.MatchTemplate(imgScene, template, imgout, Emgu.CV.CvEnum.TemplateMatchingType.CcorrNormed);

                double minVal = 0.0;
                double maxVal = 0.0;
                System.Drawing.Point minLoc = new System.Drawing.Point();
                System.Drawing.Point maxLoc = new System.Drawing.Point();

                CvInvoke.MinMaxLoc(imgout, ref minVal, ref maxVal, ref minLoc, ref maxLoc);
                Rectangle r = new Rectangle(maxLoc, template.Size);
                var imgCrop = imgScene.Copy(r);
                CvInvoke.Rectangle(imgScene, r, new MCvScalar(0, 0, 255), 2);

                if (pathSave != null)
                {
                    imgScene.Save(pathSave);
                }
                return imgCrop.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("E007 " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnOCR_Click(object sender, EventArgs e)
        {

            // Matching
            this.richTextBox1.Text = string.Empty;
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string nameTemp = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + fileName;
            pictureBoxCamera1.Image.Save(nameTemp, ImageFormat.Jpeg);
            //_pathFile = System.IO.Path.Combine(_path_defult, "System", "temp", fileName);
            inputfilenameimage = nameTemp;


            richTextBox1.Text = string.Empty;
            settings1 = SC_M2_V2._00.Modules.Setting.GetSetting(0);
            string file_name = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + Guid.NewGuid().ToString() + ".jpg";

            var imagemaster = new Bitmap(settings1[0].path_image);
            var imageCam = new Bitmap(inputfilenameimage);
            Bitmap bitmap = Matching(imagemaster, imageCam);
            bitmap.Save(file_name);

            pictureBoxCamDetect1.Image = Image.FromFile(file_name);
            inputfilename = file_name;
            imageList = new List<Image>();

            imageList.Add(pictureBoxCamDetect1.Image);

            Rectangle rect = this.pictureBoxCamDetect1.GetRect();
            string result = string.Empty;
            if (rect != Rectangle.Empty)
            {
                try
                {
                    rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                    result = performOCR(imageList, file_name, imageIndex, rect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                result = performOCR(imageList, file_name, imageIndex, Rectangle.Empty);
            }

            richTextBox1.Text = result.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");
            btnOCR2.PerformClick();
        }

        private void FuncOCR_()
        {
            // Matching
            this.richTextBox1.Text = string.Empty;
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string nameTemp = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + fileName;
            pictureBoxCamera1.Image.Save(nameTemp, ImageFormat.Jpeg);
            //_pathFile = System.IO.Path.Combine(_path_defult, "System", "temp", fileName);
            inputfilenameimage = nameTemp;


            richTextBox1.Text = string.Empty;
            settings1 = SC_M2_V2._00.Modules.Setting.GetSetting(0);
            string file_name = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + Guid.NewGuid().ToString() + ".jpg";

            var imagemaster = new Bitmap(settings1[0].path_image);
            var imageCam = new Bitmap(inputfilenameimage);
            Bitmap bitmap = Matching(imagemaster, imageCam);
            bitmap.Save(file_name);

            pictureBoxCamDetect1.Image = Image.FromFile(file_name);
            inputfilename = file_name;
            imageList = new List<Image>();

            imageList.Add(pictureBoxCamDetect1.Image);

            Rectangle rect = this.pictureBoxCamDetect1.GetRect();
            string result = string.Empty;
            if (rect != Rectangle.Empty)
            {
                try
                {
                    rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                    result = performOCR(imageList, file_name, imageIndex, rect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                result = performOCR(imageList, file_name, imageIndex, Rectangle.Empty);
            }

            richTextBox1.Text = result.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");
            FuncOCR2_();
        }

        private void backgroundWorkerOcr_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            OCRImageEntity entity = (OCRImageEntity)e.Argument;
            entity.Language = "eng";
            OCR<Image> ocrEngine = new OCRImages();
            ocrEngine.PageSegMode = selectedPSM;
            ocrEngine.OcrEngineMode = selectedOEM;
            ocrEngine.Language = entity.Language;

            IList<Image> images = entity.ClonedImages;

            for (int i = 0; i < images.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                string result = ocrEngine.RecognizeText(((List<Image>)images).GetRange(0, 1), entity.Inputfilename);
                Console.WriteLine(result);
                //worker.ReportProgress(i, result); // i is not really percentage
                //Console.WriteLine(result);
            }
        }
        SettingModel settingModel;

        private void masterModelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (settingModel != null)
            {
                settingModel.Close();
                settingModel.Dispose();
                settingModel = null;
            }
            settingModel = new SettingModel();
            settingModel.Show();

        }

        private void btnOCR2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = string.Empty;
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string nameTemp = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + fileName;
            pictureBoxCamera2.Image.Save(nameTemp, ImageFormat.Jpeg);
            //_pathFile = System.IO.Path.Combine(_path_defult, "System", "temp", fileName);
            inputfilenameimage2 = nameTemp;

            settings2 = SC_M2_V2._00.Modules.Setting.GetSetting(1);
            string file_name = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + Guid.NewGuid().ToString() + ".jpg";
            Bitmap bitmap = Matching(new Bitmap(settings2[0].path_image), new Bitmap(inputfilenameimage2));
            bitmap.Save(file_name);
            pictureBoxCamDetect2.Image = Image.FromFile(file_name);
            inputfilename = file_name;

            imageList = new List<Image>();

            imageList.Add(pictureBoxCamDetect2.Image);
            Rectangle rect = this.pictureBoxCamDetect1.GetRect();

            string result = "";
            if (rect != Rectangle.Empty)
            {
                try
                {
                    rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                    result = performOCR(imageList, file_name, imageIndex, rect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                result = performOCR(imageList, file_name, imageIndex, Rectangle.Empty);
            }

            richTextBox2.Text = result.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");

            Compare_Master(richTextBox1.Text.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", ""), richTextBox2.Text.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", ""));
        }
       
        private void FuncOCR2_()
        {
            richTextBox2.Text = string.Empty;
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string nameTemp = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + fileName;
            pictureBoxCamera2.Image.Save(nameTemp, ImageFormat.Jpeg);
            //_pathFile = System.IO.Path.Combine(_path_defult, "System", "temp", fileName);
            inputfilenameimage2 = nameTemp;

            settings2 = SC_M2_V2._00.Modules.Setting.GetSetting(1);
            string file_name = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + Guid.NewGuid().ToString() + ".jpg";
            Bitmap bitmap = Matching(new Bitmap(settings2[0].path_image), new Bitmap(inputfilenameimage2));
            bitmap.Save(file_name);
            pictureBoxCamDetect2.Image = Image.FromFile(file_name);
            inputfilename = file_name;

            imageList = new List<Image>();

            imageList.Add(pictureBoxCamDetect2.Image);
            Rectangle rect = this.pictureBoxCamDetect1.GetRect();

            string result = "";
            if (rect != Rectangle.Empty)
            {
                try
                {
                    rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                    result = performOCR(imageList, file_name, imageIndex, rect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                result = performOCR(imageList, file_name, imageIndex, Rectangle.Empty);
            }

            richTextBox2.Text = result.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");

            Compare_Master(richTextBox1.Text.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", ""), richTextBox2.Text.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", ""));
        }
        History history;
        private void Compare_Master(string txt_sw, string txt_lb)
        {
            lbTitle.Text = "Comparing...";

            var lb = txt_lb.IndexOf("731TMC");
            var txt = txt_lb.Substring(0, lb);
            var master_lb = MasterAll.GetMasterALLByLBName(txt);
            bool check = false;
            foreach (var item in master_lb)
            {
                if (item.nameSW == txt_sw)
                {
                    check = true;
                    serialCommand("OK#");
                    break;
                }
            }

            if (!check)
            {
                lbTitle.Text = "NG";
                lbTitle.ForeColor = Color.White;
                lbTitle.BackColor = Color.Red;
                is_Blink_NG = true;
                serialCommand("NG#");
            }
            else
            {
                lbTitle.Text = "OK";
                lbTitle.ForeColor = Color.White;
                lbTitle.BackColor = Color.Green;
            }
            
            history = new History();
            history.name = txtEmployee.Text.Trim();
            history.name_lb = txt_lb;
            history.name_sw = txt_sw;
            history.result = check? "OK" : "NG";
            history.Save();
            loadTableHistory();
        }

        private void textTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt_sw = "9U7310TM075-01731TMC30823030147PVM(HD)ECU86792-YY031";
            var lb = txt_sw.IndexOf("731TMC");
            var txt = txt_sw.Substring(0, lb);
            Console.WriteLine(txt);
        }

        private void backgroundWorkerOcr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var results = e;
            this.richTextBox1.AppendText((string)e.UserState);
            //serialCommand("OK#");
        }

        private void backgroundWorkerOcr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                //this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Convert a Bitmap color white to background color
        public static Bitmap ConvertWhiteToBackground(Bitmap bmp)
        {
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(bmp2);
            g.Clear(Color.White);
            g.DrawImage(bmp, 0, 0);
            g.Dispose();
            return bmp2;
        }

        private Bitmap invertColor(Bitmap bmp)
        {
            Bitmap output = new Bitmap(bmp.Width, bmp.Height);
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color pixelColor = bmp.GetPixel(x, y);
                        int invertedRed = 255 - pixelColor.R;
                        int invertedGreen = 255 - pixelColor.G;
                        int invertedBlue = 255 - pixelColor.B;
                        output.SetPixel(x, y, Color.FromArgb(invertedRed, invertedGreen, invertedBlue));
                    }
                }
            return ConvertWhiteToBackground(output);
        }

        public void loadTableHistory()
        {
            var list = History.GetHistory();
            dataGridViewHistory.DataSource = list;
            int i = 0;
            var data = (from p in list
                        select new
                        {
                            ID = p.id, 
                            No = ++i,
                            Employee = p.name,
                            Software = p.name_sw,
                            Models = p.name_lb,
                            Results = p.result,
                            Update = p.created_at
                        }).ToList();
            dataGridViewHistory.DataSource = data;
            dataGridViewHistory.Columns[0].Visible = false;
            // 10% of the width of the DataGridView
            dataGridViewHistory.Columns[1].Width = dataGridViewHistory.Width * 10 / 100;
            // last 20% of the width of the DataGridView
            dataGridViewHistory.Columns[dataGridViewHistory.Columns.Count -1].Width = dataGridViewHistory.Width * 20 / 100;
        }
    }
}