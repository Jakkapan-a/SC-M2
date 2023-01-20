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
using SC_M2_V2._00.ImageClassification;
using System.Drawing.Imaging;
using SC_M2_V2._00.Properties;
using SC_M2_V2._00;
using SC_M2_V2._00.FormComponents;
using Emgu.CV;
using Emgu.CV.Structure;
using SC_M2_V2._00.Modules;
using System.Windows.Markup;
using System.Text.RegularExpressions;

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

        public string serialportName = string.Empty;
        public string baudrate = string.Empty;

        public string dataSerialReceived = string.Empty;
        public string readDataSerial = string.Empty;

        private OpenCvSharp.VideoCapture videoCapture1;
        private OpenCvSharp.VideoCapture videoCapture2;
        private bool isVideoCapture1 = false, isVideoCapture2 = false;
        public Dictionary<string,bool> statusCamConnect = new Dictionary<string,bool>();
        _731TMC.ModelInput _731TMC_Detect_Ver = new _731TMC.ModelInput();


        List<Image> ListImage = new List<Image>();

        public bool isVideoCapture = false;
        private bool isStaetReset = true;

        private bool statevideoCapture1 = false;
        private bool statevideoCapture2 = false;

        private int countDetect = 0;


        public Home()
        {
            InitializeComponent();
        }
        SC_M2_V2_00.FormComponent.Connections connections;
        private void Home_Load(object sender, EventArgs e)
        {
            statusCamConnect.Clear();
            statusCamConnect.Add("connect", true);
            statusCamConnect["connect"] = false;
            timerMain.Start();

            if (!Directory.Exists(SC_M2_V2._00.Properties.Resources.Path_System_Temp))
            {
                Directory.CreateDirectory(SC_M2_V2._00.Properties.Resources.Path_System_Temp);
            }

        }

        private void conectionsToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            if(connections != null)
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
                this.serialCommand("conn");
                Thread.Sleep(100);
                this.serialCommand("OK#");
                this.toolStripStatusConnectSerialPort.Text = "Serial Port: Connected";
                this.toolStripStatusConnectSerialPort.ForeColor = Color.Green;
            }
            catch(Exception ex)
            {
                this.toolStripStatusConnectSerialPort.Text = "Serial Port: Disconnect";
                this.toolStripStatusConnectSerialPort.ForeColor = Color.Red;
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
          
        }

        public void serialConnect(){
            
            if(this.serialportName == string.Empty || this.baudrate == string.Empty){
                MessageBox.Show("Please select serial port and baud rate", "Exclamation", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            this.serialConnect(this.serialportName, int.Parse(this.baudrate));
        }

        public void serialCommand(string command)
        {
            if(this.serialPort.IsOpen)
            {
                this.serialPort.Write(">"+command+"<#");
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                readDataSerial = this.serialPort.ReadExisting();
                this.Invoke(new EventHandler(dataReceived));
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataReceived(object sender, EventArgs e)
        {
            this.dataSerialReceived += readDataSerial;
            //Console.WriteLine("Read : " + dataSerialReceived);
            if (dataSerialReceived.Contains(">") && dataSerialReceived.Contains("<"))
            {   
                string data = this.dataSerialReceived.Replace("\r",string.Empty).Replace("\n",string.Empty);
                data = data.Substring(data.IndexOf(">") + 1, data.IndexOf("<") - 1);
                this.dataSerialReceived = string.Empty;

                Console.WriteLine("Received ->: "+data);
                if(data == "rst")
                {
                    isStaetReset = true;
                }

                //serialCommand("OK#");
            }
            else if(!dataSerialReceived.Contains(">"))
            {
                this.dataSerialReceived = string.Empty;
            }
        }

        #endregion

        private void setCameraDevice(int device1, int device2)
        {
            if(device1 == -1 || device2 == -1)
            {
                MessageBox.Show("Please select camera device", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.deviceCamera1 = device1;
            this.deviceCamera2 = device2;
            //this.cameraConnect();
        }


        private void cameraConnect()
        {
            try
            {
                statevideoCapture1 = false;
                statevideoCapture2 = false;
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
                this.timerVideo1.Stop();
                this.timerVideo2.Stop();

                captureImage1.Stop();

                captureImage1.drive = this.deviceCamera1;

                captureImage1.Start();

                captureImage2.Stop();

                captureImage2.drive = this.deviceCamera2;

                captureImage2.Start();


                #region Code old
                //this.videoCapture1 = new OpenCvSharp.VideoCapture(this.deviceCamera1);
                //this.videoCapture2 = new OpenCvSharp.VideoCapture(this.deviceCamera2);

                //this.videoCapture1.Open(this.deviceCamera1);
                //Thread.Sleep(50);
                //this.videoCapture2.Open(this.deviceCamera2);
                //  

                //setVideoCapture(this.videoCapture1, int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth), int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight));
                // setVideoCapture(this.videoCapture2, int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth), int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight));


                //this.videoCapture2.FrameWidth = int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth);
                //this.videoCapture2.FrameHeight = int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight);

                //isVideoCapture1 = false;
                //Thread.Sleep(1000);
                //Task.Run(async ()=>
                //{

                //    this.videoCapture1 = new OpenCvSharp.VideoCapture(this.deviceCamera1);
                //    this.videoCapture1.Open(this.deviceCamera1);
                //    setVideoCapture(this.videoCapture1, int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth), int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight));
                //    isVideoCapture1 = true;
                //    while (true)
                //    {
                //        try
                //        {
                //            //Console.WriteLine("Run..");
                //            if (videoCapture1 != null && videoCapture1.IsOpened())
                //            {
                //                using (OpenCvSharp.Mat frame = videoCapture1.RetrieveMat())
                //                {

                //                    if (frame != null)
                //                    {
                //                        pictureBoxCamera1.SuspendLayout();
                //                        pictureBoxCamera1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                //                        pictureBoxCamera1.ResumeLayout();
                //                    }
                //                }
                //            }

                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //            break;
                //        }
                //        await Task.Delay(100);
                //        if(isVideoCapture1==false)
                //        {
                //            break;
                //        }
                //    }

                //});

                //isVideoCapture2 = false;
                //Thread.Sleep(1000);
                //Task.Run(async () =>
                //{

                //    this.videoCapture2 = new OpenCvSharp.VideoCapture(this.deviceCamera2);
                //    this.videoCapture2.Open(this.deviceCamera1);
                //    setVideoCapture(this.videoCapture2, int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth), int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight));
                //    isVideoCapture2 = true;
                //    while (true)
                //    {
                //        try
                //        {
                //            //Console.WriteLine("Run..");
                //            if (videoCapture2 != null && videoCapture2.IsOpened())
                //            {
                //                using (OpenCvSharp.Mat frame = videoCapture2.RetrieveMat())
                //                {

                //                    if (frame != null)
                //                    {
                //                        pictureBoxCamera2.SuspendLayout();
                //                        pictureBoxCamera2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                //                        pictureBoxCamera2.ResumeLayout();
                //                    }
                //                }
                //            }

                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //            break;
                //        }
                //        await Task.Delay(100);
                //        if (isVideoCapture2 == false)
                //        {
                //            break;
                //        }
                //    }

                //});

                //this.timerVideo1.Interval = 1000 / 10;
                //this.timerVideo2.Interval = 1000 / 10;

                //statevideoCapture1 = true;
                //statevideoCapture2 = true;
                //Console.WriteLine("End "+DateTime.Now.ToString("HH:mm:ss"));
                //this.timerVideo1.Start();
                //this.timerVideo2.Start();
                #endregion
                Console.WriteLine("End " + DateTime.Now.ToString("HH:mm:ss"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }
       
        
        private static void setVideoCapture(OpenCvSharp.VideoCapture capture,int FrameWidth , int FrameHeight)
        {
            capture.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, FrameWidth);
            capture.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, FrameHeight);
        }

        private void disposeCamera(){
            try{
                if(this.videoCapture1 != null)
                {
                    if(this.videoCapture1.IsOpened())
                    {
                        this.videoCapture1.Release();
                    }
                    //this.timerVideo1.Stop();
                    //this.videoCapture1.Dispose();
                    //this.videoCapture1 = null;
                }
                if(this.videoCapture2 != null)
                {
                    if(this.videoCapture2.IsOpened())
                    {
                        this.videoCapture2.Release();
                    }
                    //this.timerVideo2.Stop();
                    this.videoCapture2.Dispose();
                    this.videoCapture2 = null;
                }
                this.toolStripStatusConnectionCamera.Text = $"Camera 1 : Disconnected | Camera 2 : Disconnected";
                this.toolStripStatusConnectionCamera.ForeColor = Color.Red;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void timerMain_Tick(object sender, EventArgs e)
        {
            countDetect++;
            if(countDetect>10000)
            { countDetect=0; }

            if(this.videoCapture1 != null && this.videoCapture1.IsOpened() && statevideoCapture1 == true)
            {
                this.timerVideo1.Start();
                //Console.WriteLine("V...1");
                statevideoCapture1 = false;
                this.lbTitle.Text = "------------";

                this.toolStripStatusConnectionCamera.Text = SC_M2_V2._00.Properties.Resources.CamConnected;
                this.toolStripStatusConnectionCamera.ForeColor = Color.Green;
                this.statusCamConnect["connect"] = true;
            }
            
            if (this.videoCapture2 != null && this.videoCapture2.IsOpened() && statevideoCapture2 == true)
            {
                this.timerVideo2.Start();
                //Console.WriteLine("V...2");
                statevideoCapture2 = false;
                this.lbTitle.Text = "------";
                this.toolStripStatusConnectionCamera.Text = SC_M2_V2._00.Properties.Resources.CamConnected;
                this.toolStripStatusConnectionCamera.ForeColor = Color.Green;
                this.statusCamConnect["connect"] = true;
            }          
        }

        private void timerVideo1_Tick(object sender, EventArgs e)
        {
            try
            {
                if(videoCapture1!= null && videoCapture1.IsOpened())
                {
                    using(OpenCvSharp.Mat frame = videoCapture1.RetrieveMat())
                    {
                        //frame = videoCapture1.RetrieveMat();
                        if(frame != null)
                        {
                            pictureBoxCamera1.SuspendLayout();
                            pictureBoxCamera1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                            pictureBoxCamera1.ResumeLayout();
                            if(countDetect > 3)
                            {
                                
                                if(_731TMC_Detect_Ver != null && isStaetReset)
                                {
                                    string nameTemp = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + Guid.NewGuid().ToString() + ".jpg";
                                    pictureBoxCamera1.Image.Save(nameTemp, ImageFormat.Jpeg);
                                    var imageBytes = File.ReadAllBytes(nameTemp);
                                    _731TMC_Detect_Ver.ImageSource = imageBytes;

                                    var predictionResult = _731TMC.Predict(_731TMC_Detect_Ver);
                                    //Console.WriteLine($"\nPredicted Label value: {predictionResult.PredictedLabel} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n");
                                    //Console.WriteLine("Stop Time" + DateTime.Now.ToString("HH:mm:ss"));
                                    //Console.WriteLine("------------------------------------");
                                    if(predictionResult.PredictedLabel == "VER" && predictionResult.Score[0] > 0.7)
                                    {
                                        isStaetReset = false;
                                        inputfilenameimage = nameTemp;
                                        //timerRunOCR.Start();
                                        //MessageBox.Show("OK......Image : "+ inputfilenameimage);
                                        btnOCR.PerformClick();
                                    }
                                    else
                                    {
                                        //Console.WriteLine("Not Found : " + predictionResult.Score[0]);
                                        if (File.Exists(nameTemp))
                                        {
                                            File.Delete(nameTemp);
                                        }
                                    }
                                    imageBytes = null;
                                   
                                }
                                countDetect=0;
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                //this.timerVideo1.Stop();
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void timerVideo2_Tick(object sender, EventArgs e)
        {
            try
            {
                //Console.WriteLine("Run..");
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
                this.timerVideo2.Stop();
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.disposeCamera();
            statusCamConnect["connect"] = false;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cameraConnect();
            //statusCamConnect["connect"] = true;
            //this.timerStartStop.Start();
            this.lbTitle.Text = "Camera are opening.";
            //backgroundWorkerOpenCamera.RunWorkerAsync();
            //this.backgroundWorkerCamera.RunWorkerAsync();
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
           if(options!= null)
            {
                options.Dispose();
                options= null;
            }

            if (videoCapture1 != null &&videoCapture1.IsOpened())
            {
                videoCapture1.Release();
                videoCapture1.Dispose();
                videoCapture1 = null;
            }

            if (videoCapture2 != null && videoCapture2.IsOpened())
            {
                videoCapture2.Release();
                videoCapture2.Dispose();
                videoCapture2 = null;
            }
           options = new Options();
           options.Show(this);
        }

        private void performOCR(IList<Image> imageList, string inputfilename, int index, Rectangle rect)
        {
            try
            {
                if (curLangCode.Trim().Length == 0)
                {
                    MessageBox.Show(this, "curLangCode = 0");
                    return;
                }
                OCRImageEntity entity = new OCRImageEntity(imageList, inputfilename, index, rect, curLangCode);
                //entity.ScreenshotMode = this.screenshotModeToolStripMenuItem.Checked;
                entity.ScreenshotMode = false;
                //backgroundWorkerOcr.RunWorkerAsync(entity);

                entity.Language = "eng";
                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PageSegMode = selectedPSM;
                ocrEngine.OcrEngineMode = selectedOEM;
                ocrEngine.Language = entity.Language;

                IList<Image> images = entity.ClonedImages;

                //for (int i = 0; i < images.Count; i++)
                //{
                //    if (worker.CancellationPending)
                //    {
                //        e.Cancel = true;
                //        break;
                //    }

               string result = ocrEngine.RecognizeText(((List<Image>)images).GetRange(0, 1), entity.Inputfilename);
                //Console.WriteLine(result);
                this.richTextBox1.AppendText(result);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        List<Setting> settings1 = new List<Setting>();
        List<Setting> settings2 = new List<Setting>();

        private void Process(string filename)
        {

        }

        private void backgroundWorkerOpenCamera_DoWork(object sender, DoWorkEventArgs e)
        {
            cameraConnect();
            toolStripStatusConnectionCamera.Text = SC_M2_V2._00.Properties.Resources.CamConnecting;
            toolStripStatusConnectionCamera.ForeColor = Color.Orange;     
        }

        private void backgroundWorkerOpenCamera_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorkerOpenCamera_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Console.WriteLine("backgroundWorkerOpenCamera_RunWorkerCompleted");

            //this.timerVideo1.Start();
            //this.timerVideo2.Start();
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

        private void timerRunOCR_Tick(object sender, EventArgs e)
        {
            //timerRunOCR.Stop();
            //Process(inputfilenameimage);
        }

        private void btnOCR_Click(object sender, EventArgs e)
        {

            // Matching
            this.richTextBox1.Text = string.Empty;
            settings1 = Setting.GetSetting(0);
            if (inputfilenameimage == string.Empty)
                return;

            richTextBox1.Text = string.Empty;
            string file_name = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + Guid.NewGuid().ToString() + ".jpg";
            Bitmap bitmap = Matching(new Bitmap(settings1[0].path_image), new Bitmap(inputfilenameimage));
            bitmap.Save(file_name);
            pictureBoxCamDetect1.Image = Image.FromFile(file_name);
            inputfilename = file_name;
            imageList = new List<Image>();
            imageList.Add(pictureBoxCamDetect1.Image);

            Rectangle rect = this.pictureBoxCamDetect1.GetRect();

            if (rect != Rectangle.Empty)
            {
                try
                {
                    rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                    performOCR(imageList, inputfilename, imageIndex, rect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                performOCR(imageList, inputfilename, imageIndex, Rectangle.Empty);
            }

            //performOCR(imageList, inputfilename, 0, Rectangle.Empty);

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

 
    }


}
