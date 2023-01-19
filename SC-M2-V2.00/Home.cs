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
namespace SC_M2_V2_00
{
    public partial class Home : Form
    {
        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };
        public int deviceCamera1 = -1;
        public int deviceCamera2 = -1;

        public string serialportName = string.Empty;
        public string baudrate = string.Empty;

        public string dataSerialReceived = string.Empty;
        public string readDataSerial = string.Empty;

        public OpenCvSharp.VideoCapture videoCapture1;
        public OpenCvSharp.VideoCapture videoCapture2;

        public Dictionary<string,bool> statusCamConnect = new Dictionary<string,bool>();
        _731TMC.ModelInput _731TMC_Detect_Ver = new _731TMC.ModelInput();
        Thread thread = new Thread(new ThreadStart(openCamera));

        public bool isVideoCapture = false;


        private static void openCamera()
        {
            //throw new NotImplementedException();
        }

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

            this.videoCapture1 = new OpenCvSharp.VideoCapture();
            this.videoCapture2= new OpenCvSharp.VideoCapture();
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
            if(dataSerialReceived.Contains(">") && dataSerialReceived.Contains("<"))
            {   
                string data = this.dataSerialReceived.Replace("\r",string.Empty).Replace("\n",string.Empty);
                data = data.Substring(data.IndexOf(">") + 1, data.IndexOf("<") - 1);
                this.dataSerialReceived = string.Empty;

                Console.WriteLine("Received : "+data);

            }else if(!dataSerialReceived.Contains(">"))
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
                    MessageBox.Show("Please select driver of camera!", "Exclamation", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
                this.timerVideo1.Stop();
                this.timerVideo2.Stop();
                 
                this.videoCapture1 = new OpenCvSharp.VideoCapture(this.deviceCamera1);
                this.videoCapture2 = new OpenCvSharp.VideoCapture(this.deviceCamera2);

                this.videoCapture1.Open(this.deviceCamera1);
                //Thread.Sleep(50);
                this.videoCapture2.Open(this.deviceCamera2);
                //  
                this.videoCapture1.FrameWidth = int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth);
                this.videoCapture1.FrameHeight = int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight);

                this.videoCapture2.FrameWidth = int.Parse(SC_M2_V2._00.Properties.Resources.FrameWidth);
                this.videoCapture2.FrameHeight = int.Parse(SC_M2_V2._00.Properties.Resources.FrameHeight);

                this.timerVideo1.Interval = 1000 / 10;
                this.timerVideo2.Interval = 1000 / 10;

                statevideoCapture1 = true;
                statevideoCapture2 = true;
                //Console.WriteLine("End "+DateTime.Now.ToString("HH:mm:ss"));
                //this.timerVideo1.Start();
                //this.timerVideo2.Start();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        private void setVideoCapture()
        {
            if(this.videoCapture1 == null || this.videoCapture2 == null)
            {
                MessageBox.Show("Please select camera device", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int width = 640;
            int height = 480;
            this.videoCapture1.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, width);
            this.videoCapture1.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, height);

            this.videoCapture2.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, width);
            this.videoCapture2.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, height);
        }

        private void disposeCamera(){
            try{
                if(this.videoCapture1 != null)
                {
                    if(this.videoCapture1.IsOpened())
                    {
                        this.videoCapture1.Release();
                    }
                    this.timerVideo1.Stop();
                    this.videoCapture1.Dispose();
                    this.videoCapture1 = null;
                }
                if(this.videoCapture2 != null)
                {
                    if(this.videoCapture2.IsOpened())
                    {
                        this.videoCapture2.Release();
                    }
                    this.timerVideo2.Stop();
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
        private bool statevideoCapture1 = false;
        private bool statevideoCapture2 = false;
        private void timerMain_Tick(object sender, EventArgs e)
        {
            countDetect++;
            if(countDetect>10000)
            { countDetect=0; }

            if(this.videoCapture1 != null && this.videoCapture1.IsOpened() && statevideoCapture1 == true)
            {
                this.timerVideo1.Start();
                Console.WriteLine("V...1");
                statevideoCapture1 = false;
            }
            
            if (this.videoCapture2 != null && this.videoCapture2.IsOpened() && statevideoCapture2 == true)
            {
                this.timerVideo2.Start();
                Console.WriteLine("V...2");
                statevideoCapture2 = false;
            }
          
        }

        private void timerVideo1_Tick(object sender, EventArgs e)
        {
            try
            {
                if(videoCapture1!= null && videoCapture1.IsOpened())
                {
                    using(OpenCvSharp.Mat frame = new OpenCvSharp.Mat())
                    {
                        videoCapture1.Read(frame);
                        if(frame != null)
                        {
                            pictureBoxCamera1.SuspendLayout();
                            pictureBoxCamera1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                            pictureBoxCamera1.ResumeLayout();
                            if(countDetect > 2)
                            {
                                
                                if(_731TMC_Detect_Ver != null)
                                {

                                    //Console.WriteLine("\n------------------------------------");
                                    Console.WriteLine("Stop Time" + DateTime.Now.ToString("HH:mm:ss"));
                                    string nameTemp = SC_M2_V2._00.Properties.Resources.Path_System_Temp + "/" + Guid.NewGuid().ToString() + ".jpg";

                                    pictureBoxCamera1.Image.Save(nameTemp, ImageFormat.Jpeg);
                                    var imageBytes = File.ReadAllBytes(nameTemp);
                                    _731TMC_Detect_Ver.ImageSource = imageBytes;


                                    var predictionResult = _731TMC.Predict(_731TMC_Detect_Ver);
                                    //Console.WriteLine($"\nPredicted Label value: {predictionResult.PredictedLabel} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n");
                                    //Console.WriteLine("Stop Time" + DateTime.Now.ToString("HH:mm:ss"));
                                    //Console.WriteLine("------------------------------------");
                                    if(predictionResult.PredictedLabel == "VER")
                                    {
                                        
                                    }
                                    imageBytes = null;
                                    if (File.Exists(nameTemp))
                                    {
                                        File.Delete(nameTemp);
                                    }
                                }

                                //Console.WriteLine("Deteted.."+ DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                                countDetect=0;
                            }
                        }
                    }
                    //Console.WriteLine("Data : " + videoCapture1.IsOpened().ToString());
                    // if (!videoCapture1.IsOpened())
                    // {
                    // }
                }
            }catch(Exception ex)
            {
                this.timerVideo1.Stop();
                MessageBox.Show(ex.Message, "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void timerVideo2_Tick(object sender, EventArgs e)
        {
            try
            {
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
            //this.cameraConnect();
            //statusCamConnect["connect"] = true;
            //this.timerStartStop.Start();
            this.lbTitle.Text = "Camera are opening.";
            backgroundWorkerOpenCamera.RunWorkerAsync();
            //this.backgroundWorkerCamera.RunWorkerAsync();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.disposeCamera();
        }


        private void backgroundWorkerOcr_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            OCRImageEntity entity = (OCRImageEntity)e.Argument;
            entity.Language = "eng";
            OCR<Image> ocrEngine = new OCRImages();
            //ocrEngine.PageSegMode = selectedPSM;
            //ocrEngine.OcrEngineMode = selectedOEM;
            ocrEngine.Language = entity.Language;

            IList<Image> images = entity.ClonedImages;

            for (int i = 0; i < images.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                string result = ocrEngine.RecognizeText(((List<Image>)images).GetRange(i, 1), entity.Inputfilename, entity.Rect, worker, e);
                worker.ReportProgress(i, result); // i is not really percentage
            }
        }

        private void backgroundWorkerOcr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.richTextBox1.AppendText((string)e.UserState);
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
        
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
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
            //Console.WriteLine("backgroundWorkerOpenCamera_ProgressChanged");

        }

        private void backgroundWorkerOpenCamera_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Console.WriteLine("backgroundWorkerOpenCamera_RunWorkerCompleted");

            this.timerVideo1.Start();
            this.timerVideo2.Start();

            this.toolStripStatusConnectionCamera.Text = SC_M2_V2._00.Properties.Resources.CamConnected;
            this.toolStripStatusConnectionCamera.ForeColor = Color.Green;
            this.statusCamConnect["connect"] = true;
        }
    }


}
