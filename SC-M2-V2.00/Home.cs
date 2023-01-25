using OpenCvSharp;
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
using Resoure = SC_M2_V2._00.Properties.Resources;
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

        private VideoCAM videoCAM_1;
        private VideoCAM videoCAM_2;

        private int step_program = 0;


        public Home()
        {
            InitializeComponent();
        }
        SC_M2_V2_00.FormComponent.Connections connections;

        private void Home_Load(object sender, EventArgs e)
        {
            timerMain.Start();
            videoCAM_1= new VideoCAM();
            videoCAM_1.OnVideoFrameHandler += videoCAM_1_OnVideoFrame;
            videoCAM_2= new VideoCAM();
            videoCAM_2.OnVideoFrameHandler += videoCAM_2_OnVideoFrame;
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
            step_program = 1;
            lbTitle.Text = Resources.STATUS_PROCESS_4; // Please open the camera.
            btnOCR.Visible = false;
            btnOCR2.Visible = false;

            // Focus on the first txtEmployee
            this.ActiveControl = txtEmployee;
            txtEmployee.Focus();
            loadTableHistory();

            // Console thread id for debugging
            Console.WriteLine("Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
        }


        private delegate void FrameVideo(Bitmap bitmap);

        private void videoCAM_1_OnVideoFrame(Bitmap bitmap)
        {
            // If invoke is required, invoke it
            if (pictureBoxCamera1.InvokeRequired)
            {
                pictureBoxCamera1.Invoke(new FrameVideo(videoCAM_1_OnVideoFrame), bitmap );
                return;
            }else{
                pictureBoxCamera1.Image = new Bitmap(bitmap);
            }
        }

        private void videoCAM_2_OnVideoFrame(Bitmap bitmap)
        {
            // If invoke is required, invoke it
            if (pictureBoxCamera2.InvokeRequired)
            {
                pictureBoxCamera2.Invoke(new FrameVideo(videoCAM_2_OnVideoFrame), bitmap);
                return;
            }
            else
            {
                pictureBoxCamera2.Image = new Bitmap(bitmap);
            }
        }
        private void process_OutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            if (_stepImageClassification == 1)
            {
                string data = e.Data.ToString();
                if (data != "NotFound")
                {
                    var array = data.Split('-');

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
                Thread.Sleep(50);

                this.toolStripStatusConnectSerialPort.Text = Resources.STATUS_PROCESS_8;// Serial Connected
                this.toolStripStatusConnectSerialPort.ForeColor = Color.Green;

            }
            catch (Exception ex)
            {
                this.toolStripStatusConnectSerialPort.Text = Resources.STATUS_PROCESS_9;// "Serial Port: Disconnect";
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
                    lbTitle.Text = Resources.STATUS_PROCESS_5; // Wiat for detect....
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

        private void cameraConnect()
        {
            try
            {
                if (txtEmployee.Text == string.Empty)
                {
                    lbTitle.Text = Resources.STATUS_PROCES_10; // STATUS_PROCES_10
                    MessageBox.Show("Please input employee ID", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (this.deviceCamera1 == -1 || this.deviceCamera2 == -1)
                {
                    lbTitle.Text = Resources.STATUS_PROCES_11;
                    MessageBox.Show("Please select driver of camera!", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                step_program = 2;
                lbTitle.Text = Resources.STATUS_PROCES_CONNECTING; // Connecting...
                this.timerVideo1.Stop();
                //this.timerVideo2.Stop();
                //Console.WriteLine("Start OPEN" + DateTime.Now.ToString("HH:mm:ss"));
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                _ = Task.Run(() =>
                {
                    this.videoCAM_1.Start(deviceCamera1);
                    this.videoCAM_2.Start(deviceCamera2);
                });

                this.timerVideo1.Start();
                stopwatch.Stop();
                countDetect = 0;
                Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

                lbTitle.Text = Resources.STATUS_PROCESS_5;
                lbTitle.ForeColor = Color.Black;
                lbTitle.BackColor = Color.Yellow;
                toolStripStatusConnectionCamera.Text = Resources.STATUS_PROCES_CAMERA_CON; //"Camera: Connected";
                toolStripStatusConnectionCamera.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lbTitle.Text = Resources.STATUS_PROCES_DIS; // Camera can't connected.
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopCamera()
        {
            try
            {
                if(videoCAM_1!= null && videoCAM_1.IsRunning)
                {
                    videoCAM_1.Stop();
                }
                if(videoCAM_2!= null && videoCAM_2.IsRunning)
                {
                    videoCAM_2.Stop();
                }

                lbTitle.Text = Resources.STATUS_PROCES_DIS;
                this.toolStripStatusConnectionCamera.Text = Resources.STATUS_PROCES_DIS; //$"Camera: Disconnected";
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
                if (countDetect > 3 && isStaetReset && videoCAM_1.IsRunning && videoCAM_2.IsRunning)
                {
                    lbTitle.Text = Resoure.STATUS_PROCESS_5; //Waiting for detection
                    lbTitle.Refresh();
                    if (_stepImageClassification == 0 && isStaetReset)
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
                            lbTitle.Text = Resoure.STATUS_PROCES_12; // System is processing
                            //lbTitle.Text = Resoure.STATUS_PROCESS_6;
                            // OCR
                            FuncOCR_();
                            isStaetReset = false; // Wait Reset
                        }
                        else
                        {

                        }                      
                        _stepImageClassification = 0;

                        if (File.Exists(_pathFile))
                        {
                            File.Delete(_pathFile);
                        }
                        deletedFileTemp();
                    }
                    countDetect = 0;
                }
            }
            catch (Exception ex)
            {
                countDetect = 0;
                //this.timerVideo1.Stop();
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message, "Exclamation B00", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void deletedFileTemp()
        {
            string _dir = SC_M2_V2._00.Properties.Resources.Path_System_Temp;

            string[] files = Directory.GetFiles(_dir);
            int i = 0;
            foreach (string file in files)
            {
                i++;
                FileInfo info = new FileInfo(file);
                if (info.LastAccessTime < DateTime.Now.AddHours(-1))
                    info.Delete();
                if (i > 10)
                    break;
            }
            i = 0;
            files.Reverse();
            foreach (string file in files)
            {
                i++;
                FileInfo info = new FileInfo(file);
                if (info.LastAccessTime < DateTime.Now.AddHours(-1))
                    info.Delete();
                if (i > 10)
                    break;
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
          
            _streamWriterCMD.WriteLine("exit");
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }

            this.StopCamera();
            if (videoCAM_1 != null)
            {
               videoCAM_1.Dispose();
            }

            if (videoCAM_2 != null)
            {
                videoCAM_2.Dispose();
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            this.cameraConnect();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StopCamera();
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
            StopCamera();
            
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

        public double Compare(Bitmap master, Bitmap slave)
        {
            try
            {
                // 
                string name_master = Guid.NewGuid().ToString() + ".jpg";
                name_master = Path.Combine(Resoure.Path_System_Temp,name_master);
                master.Save(name_master, ImageFormat.Jpeg);

                string name_slave = Guid.NewGuid().ToString() + ".jpg";
                name_slave = Path.Combine(Resoure.Path_System_Temp, name_slave);
                slave.Save(name_slave, ImageFormat.Jpeg);

                Image<Gray, byte> imageMaster = new Image<Gray, byte>(name_master);
                Image<Gray, byte> imageSlave = new Image<Gray, byte>(name_slave);
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
                imageMaster.Dispose();
                imageSlave.Dispose();

                if (File.Exists(name_master))
                {
                    File.Delete(name_master);
                }
                if (File.Exists(name_slave))
                {
                    File.Delete(name_slave);
                }

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
            double conpare = Compare(imagemaster, bitmap);
                
            if(conpare < 50)
            {
                lbTitle.Text = Resoure.STATUS_PROCES_NOT_MATCH; //"Image not match";
                serialCommand("NG#");
                countDetect = 0;
                return;
            }

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
            Bitmap imagemaster = new Bitmap(settings2[0].path_image);
            Bitmap bitmap = Matching(imagemaster, new Bitmap(inputfilenameimage2));
            bitmap.Save(file_name);
            pictureBoxCamDetect2.Image = Image.FromFile(file_name);
            inputfilename = file_name;

            double conpare = Compare(imagemaster, bitmap);

            if (conpare < 50)
            {
                lbTitle.Text = Resoure.STATUS_PROCES_NOT_MATCH; //"Image not match";
                serialCommand("NG#");
                countDetect = 0;
                return;
            }


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
            lbTitle.Text = Resoure.STATUS_PROCES_12; // System is processing

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
            // Reverse the list to display the latest record first
            list.Reverse();
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