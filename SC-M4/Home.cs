using DirectShowLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCapture;
using SC_M4.Properties;
using System.Drawing.Imaging;
using SC_M4.Forms;
using SC_M4.OCR;
using SC_M4.Ocr;
using System.Text.RegularExpressions;
using SC_M4.Modules;
using LogWriter;

namespace SC_M4
{
    public partial class Home : Form
    {
        public TCapture.Capture capture_1;
        public TCapture.Capture capture_2;
        private Thread thread;
        //private SC_M2_V2_00.Home HomeV2;
        public Image imageCamaera_01, imageCamaera_02;
        public Bitmap bitmapCamaera_01;
        public Bitmap bitmapCamaera_02;

        public MemoryStream memoryCamaera_01;
        public MemoryStream memoryCamaera_02;

        public Stream streamCamaera_01;

        protected string curLangCode = "eng";
        protected IList<Image> imageList;
        protected string inputfilename;
        protected int imageIndex;
        protected float scaleX = 1f;
        protected float scaleY = 1f;
        protected string selectedPSM = "Auto"; // 3 - Fully automatic page segmentation, but no OSD (default)
        protected string selectedOEM = "3"; // Default

        private bool toggle_blink_ng = false;
        private bool isStaetReset = false;
        LogFile LogWriter;


        public Home()
        {
            InitializeComponent();
        }

        public string[] baudList = { "9600", "19200", "38400", "57600", "115200" };

        private void Home_Load(object sender, EventArgs e)
        {
            LogWriter = new LogFile();
            LogWriter.path = Properties.Resources.path_log;
            btRefresh.PerformClick();

            capture_1 = new TCapture.Capture();
            capture_1.OnFrameHeadler += Capture_1_OnFrameHeadler;
            capture_1.OnVideoStarted += Capture_1_OnVideoStarted;
            capture_1.OnVideoStop += Capture_1_OnVideoStop;
            capture_2 = new TCapture.Capture();
            capture_2.OnFrameHeadler += Capture_2_OnFrameHeadler;
            capture_2.OnVideoStarted += Capture_2_OnVideoStarted;
            capture_2.OnVideoStop += Capture_2_OnVideoStop;

            memoryCamaera_01 = new MemoryStream();
            memoryCamaera_02 = new MemoryStream();

            streamCamaera_01 = new MemoryStream();
            if (!Directory.Exists(Properties.Resources.path_temp))
                Directory.CreateDirectory(Properties.Resources.path_temp);
            if (!Directory.Exists(Properties.Resources.path_log))
                Directory.CreateDirectory(Properties.Resources.path_log);
            if (!Directory.Exists(Properties.Resources.path_images))
                Directory.CreateDirectory(Properties.Resources.path_images);

            //HomeV2 = new SC_M2_V2_00.Home();

            foreach (ToolStripItem item in statusStripHome.Items)
            {
                item.Text = "";
            }
            LogWriter.SaveLog("Starting....." + Thread.CurrentThread.ManagedThreadId);
            timerMain.Start();
            deletedFileTemp();
            loadTableHistory();
            try
            {
                var s = Setting.GetSettingRemove();
                if (s.Count > 0)
                {
                    foreach (var set in s)
                    {
                        if (File.Exists(set.path_image))
                        {
                            File.Delete(set.path_image);
                            set.Delete();
                        }
                    }
                }
            } catch (Exception ex)
            {
                LogWriter.SaveLog("Error delete file : "+ ex.Message);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            var videoDevices = new List<DsDevice>(DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice));
            comboBoxCamera1.Items.Clear();
            comboBoxCamera2.Items.Clear();
            foreach (DsDevice device in videoDevices)
            {
                comboBoxCamera1.Items.Add(device.Name);
                comboBoxCamera2.Items.Add(device.Name);
            }

            if (comboBoxCamera1.Items.Count > 0)
            {
                comboBoxCamera1.SelectedIndex = 0;
                comboBoxCamera2.SelectedIndex = 0;
            }

            comboBoxBaud.Items.Clear();
            comboBoxBaud.Items.AddRange(this.baudList);
            if (comboBoxBaud.Items.Count > 0)
                comboBoxBaud.SelectedIndex = comboBoxBaud.Items.Count - 1;

            comboBoxCOMPort.Items.Clear();
            comboBoxCOMPort.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxCOMPort.Items.Count > 0)
                comboBoxCOMPort.SelectedIndex = 0;


        }

        private delegate void Stop_video();

        private void Capture_2_OnVideoStop()
        {
            LogWriter.SaveLog("Video 2 Stop");
        }

        private void Capture_2_OnVideoStarted()
        {
            Console.WriteLine("Cam 2 Started");
            LogWriter.SaveLog("Video 2 Started");
            Invoke(new Action(() => {
                scrollablePictureBoxCamera02.Image = null;
                numericUpDownFocus.Value = capture_2.GetFocus();
                }));
            
        }

        private delegate void FrameRate(Bitmap bitmap);

        private void Capture_2_OnFrameHeadler(Bitmap bitmap)
        {
            if (pictureBoxCamera02.InvokeRequired)
            {
                pictureBoxCamera02.Invoke(new FrameRate(Capture_2_OnFrameHeadler), bitmap);
                return;
            }

            if (!isStart)
            {
                pictureBoxCamera02.Image = null;
            }
            else
            {
                pictureBoxCamera02.SuspendLayout();
                pictureBoxCamera02.Image = new Bitmap(bitmap);
                bitmapCamaera_02 = (Bitmap)pictureBoxCamera02.Image.Clone();
                pictureBoxCamera02.ResumeLayout();
            }

        }

        private void Capture_1_OnVideoStop()
        {
            LogWriter.SaveLog("Video 1 Stop");
            Invoke(new Action(() => scrollablePictureBoxCamera01.Image = null));
        }

        private void Capture_1_OnVideoStarted()
        {
            Console.WriteLine("Cam 1 Started");
            LogWriter.SaveLog("Video 1 Started");
        }


        private void Capture_1_OnFrameHeadler(Bitmap bitmap)
        {
            if (pictureBoxCamera01.InvokeRequired)
            {
                pictureBoxCamera01.Invoke(new FrameRate(Capture_1_OnFrameHeadler), bitmap);
                return;
            }
            if (!isStart)
            {
                pictureBoxCamera01.Image = null;
            }
            else
            {
                pictureBoxCamera01.SuspendLayout();
                pictureBoxCamera01.Image = new Bitmap(bitmap);
                bitmapCamaera_01 = (Bitmap)pictureBoxCamera01.Image.Clone();
                pictureBoxCamera01.ResumeLayout();
            }
        }

        #region Serial Port 
        public string serialportName = string.Empty;

        public string baudrate = string.Empty;

        public string readDataSerial = string.Empty;

        public string dataSerialReceived = string.Empty;

        public bool is_Blink_NG = false;

        private void btConnect_Click(object sender, EventArgs e)
        {
 
            btStartStop.PerformClick();
        }

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
                Thread.Sleep(50);
                this.serialCommand("conn");
                this.toolStripStatusConnectSerialPort.Text = "Serial Connected";
                this.toolStripStatusConnectSerialPort.ForeColor = Color.Green;

            }
            catch (Exception ex)
            {
                LogWriter.SaveLog("Error :" + ex.Message);
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
                LogWriter.SaveLog("Serial send : " + command);
                toolStripStatusSentData.Text = "Send : "+ command;
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
                LogWriter.SaveLog("Error serial:" + ex.Message);
                MessageBox.Show(ex.Message, "Error serial", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Console.WriteLine("RST : " + data);
                data = data.Replace(">", "").Replace("<", "");
                toolStripStatusSerialData.Text = "DATA :" + data;
                LogWriter.SaveLog("Serial Received : " + data);
                if (data == "rst" || data.Contains("rst"))
                {
                    isStaetReset = true;
                    is_Blink_NG = false;
                    if (capture_1.IsOpened && capture_2.IsOpened)
                    {
                        lbTitle.Text = "Wiat for detect...."; // Wiat for detect....
                    }
                    lbTitle.ForeColor = Color.Black;
                    lbTitle.BackColor = Color.Yellow;
                    richTextBox1.Text = "";
                    richTextBox2.Text = "";
                    scrollablePictureBoxCamera01.Image = null; scrollablePictureBoxCamera02.Image = null;
                }
            }
            else if (!dataSerialReceived.Contains(">"))
            {
                this.dataSerialReceived = string.Empty;
            }
        }

        #endregion


        private bool isStart = false;
        private int driveindex_01 = -1;
        private int driveindex_02 = -1;

        private void btStartStop_Click(object sender, EventArgs e)
        {
            this.isStart = !this.isStart;
            try
            {

                if (this.isStart)
                {

                    if (txtEmployee.Text == string.Empty)
                    {
                        lbTitle.Text = "Please input employee ID"; // STATUS_PROCES_10
                        MessageBox.Show("Please input employee ID", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (comboBoxCamera1.SelectedIndex == comboBoxCamera2.SelectedIndex)
                    {
                        lbTitle.Text = "Please select camera drive!"; // 
                        MessageBox.Show("Please select camera drive!", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (this.comboBoxCamera1.SelectedIndex == -1 || this.comboBoxCamera2.SelectedIndex == -1)
                    {
                        throw new Exception(Properties.Resources.msg_select_camera);
                    }
                    if (txtEmployee.Text == string.Empty)
                    {
                        lbTitle.Text = "Please input employee ID"; // 
                        MessageBox.Show("Please input employee ID", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    this.serialportName = comboBoxCOMPort.Text;
                    this.baudrate = comboBoxBaud.Text;
                    serialConnect();

                    if (capture_1.IsOpened)
                        capture_1.Stop();
                    if (capture_2.IsOpened)
                        capture_2.Stop();
                    //openCamera();
                    driveindex_01 = comboBoxCamera1.SelectedIndex;
                    driveindex_02 = comboBoxCamera2.SelectedIndex;
                    Task.Factory.StartNew(() => capture_1.Start(driveindex_01));
                    Task.Factory.StartNew(() => capture_2.Start(driveindex_02));

                    lbTitle.Text = "Camera opening...";
   
                    btStartStop.Text = "STOP";
                    if (thread != null)
                    {
                        thread.Abort();
                        thread.DisableComObjectEagerCleanup();
                        thread = null;
                    }

                    thread = new Thread(new ThreadStart(ProcessTesting));
                    thread.Start();
                    this.richTextBox1.Text = string.Empty;
                    this.richTextBox2.Text = string.Empty;
                    
                    scrollablePictureBoxCamera01.Image = null;
                    scrollablePictureBoxCamera02.Image = null;

                    btConnect.Text = "Disconnect";
                }
                else
                {
                    if (capture_1._isRunning)
                        capture_1.Stop();

                    if (capture_2._isRunning)
                        capture_2.Stop();

                    btStartStop.Text = "START";
                    btConnect.Text = "Connect";
                    pictureBoxCamera01.Image = null;
                    pictureBoxCamera02.Image = null;

                    this.richTextBox1.Text = string.Empty;
                    this.richTextBox2.Text = string.Empty;

                    scrollablePictureBoxCamera01.Image = null;
                    scrollablePictureBoxCamera02.Image = null;
                    lbTitle.Text = "Camera close.";
                    if (thread != null)
                    {
                        thread.Abort(true);
                    }
                }
                lbTitle.BackColor = Color.Yellow;
                lbTitle.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                LogWriter.SaveLog("Error Start :" + ex.Message);
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.isStart = false;
                btStartStop.Text = "START";

            }
        }
        private void ProcessTesting()
        {
            var _masterList_01 = Modules.Setting.GetListImage(0);
            var _masterList_02 = Modules.Setting.GetListImage(1);

            // This thread working in her
            // While loop -> detect came
            TCapture.Match match = new TCapture.Match();
            Bitmap bitmap;
            Bitmap mat;
            double score = 0;
            string result = string.Empty;
            isStaetReset = true;
            bool detection = false;

                while (true)
                 {
                    try
                    {
                    if (capture_1._isRunning && capture_2._isRunning && bitmapCamaera_01 != null && bitmapCamaera_02 != null && isStaetReset)
                    {
                        detection = !detection;
                        if (detection)
                        {
                            Invoke(new Action(() =>
                            {
                                lbTitle.Text = "Wiat for detect..";
                            }));
                        }
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                lbTitle.Text = "Detecting...";
                            }));
                        }

                        foreach (var item in _masterList_01)
                        {
                            if (!isStaetReset)
                            {
                                continue;
                            }
                        // Get Bitmap 
                        // bitmap = (Bitmap)bitmapCamaera_01.Clone();
                        //bitmap = new Bitmap(bitmapCamaera_01);
                        memoryCamaera_01.Close();
                        memoryCamaera_01 = new MemoryStream();
                        bitmapCamaera_01.Save(memoryCamaera_01, ImageFormat.Jpeg);
                            bitmap = new Bitmap(memoryCamaera_01);
                            string filename_temp_1 = getFileTemp();

                            mat = TCapture.Match.Matching(new Bitmap(item.path_image), bitmap);
                            filename_temp_1 = getFileTemp();
                            LogWriter.SaveLog("File temp 1 :" + filename_temp_1);
                            
                            score = TCapture.Match.CompareImage(new Bitmap(item.path_image), new Bitmap(mat));
                            LogWriter.SaveLog("Rate 1 :" + score);
                            Console.WriteLine("Score 01: {0}", score);
                            if (score > item.percent)
                            {
                                mat.Save(filename_temp_1);
                                scrollablePictureBoxCamera01.Invoke(new Action(() =>
                                {
                                    scrollablePictureBoxCamera01.Image = (Image)mat.Clone();
                                }));

                                LogWriter.SaveLog("Caompare 1 :" + score);
                                // OCR 1 
                                Invoke(new Action(() =>
                                {
                                    this.richTextBox1.Text = string.Empty;
                                    this.richTextBox2.Text = string.Empty;
                                }));

                                Thread.Sleep(50);

                                imageList = new List<Image>();
                                imageList.Add(scrollablePictureBoxCamera01.Image);

                                Rectangle rect = this.scrollablePictureBoxCamera01.GetRect();
                                result = string.Empty;
                                if (rect != Rectangle.Empty)
                                {
                                    try
                                    {
                                        rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                                        result = performOCR(imageList, inputfilename, imageIndex, rect);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.StackTrace);
                                    }
                                }
                                else
                                {
                                    result = performOCR(imageList, inputfilename, imageIndex, Rectangle.Empty);
                                }
                                var a = result.IndexOf("-731");
                                result = result.Substring(a + 1);
                                a = result.IndexOf("|731");
                                result = result.Substring(a + 1);
                                result = result.Replace("T31TM", "731TM");
                                result = result.Replace("731THC", "731TMC");

                                if (result == string.Empty)
                                {
                                    continue;
                                }

                                richTextBox1.Invoke(new Action(() =>
                                {
                                    this.richTextBox1.Text = result.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");
                                }));
                                // Image 02
                                foreach (var item_2 in _masterList_02)
                                {
                                    if (!isStaetReset)
                                    {
                                        LogWriter.SaveLog("Continue 2 ");
                                        continue;
                                    }

                                    bitmap = null;
                                //bitmap = (Bitmap)bitmapCamaera_02.Clone();
                                    memoryCamaera_02.Close();
                                    memoryCamaera_02 = new MemoryStream();
                                    bitmapCamaera_02.Save(memoryCamaera_02,ImageFormat.Jpeg);
                                    bitmap = new Bitmap(memoryCamaera_02);
                                    string filename_temp_2 = getFileTemp();
                                    LogWriter.SaveLog("File temp 2 :" + filename_temp_2);
                                    mat = null;
                                    mat = TCapture.Match.Matching(new Bitmap(item_2.path_image), bitmap);

                                    //mat.Save(filename_temp_2);
                                    score = TCapture.Match.CompareImage(new Bitmap(item_2.path_image),new Bitmap(mat));
                                     Console.WriteLine("Score 02: {0}", score);
                                     LogWriter.SaveLog("Rate 2 :" + score);
                                    if (score > item_2.percent)
                                    {
                                        mat.Save(filename_temp_2);
                                        LogWriter.SaveLog("Caompare 2 :" + score);
                                        scrollablePictureBoxCamera02.Invoke(new Action(() =>
                                        {
                                            scrollablePictureBoxCamera02.Image = (Image)mat.Clone();
                                        }));
                                        // OCR 2
                                        imageList = new List<Image>();

                                        imageList.Add(scrollablePictureBoxCamera02.Image);

                                        rect = this.scrollablePictureBoxCamera02.GetRect();
                                        result = string.Empty;
                                        if (rect != Rectangle.Empty)
                                        {
                                            try
                                            {
                                                rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                                                result = performOCR(imageList, inputfilename, imageIndex, rect);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.StackTrace);
                                                LogWriter.SaveLog($"{ex.Message}");
                                            }
                                        }
                                        else
                                        {
                                            result = performOCR(imageList, inputfilename, imageIndex, Rectangle.Empty);
                                        }
                                        result = Regex.Replace(result, "[^a-zA-Z,0-9,(),:,-]", "");

                                        result = result.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "").Replace("'", "").Replace("|", "");
                                        result = result.Replace(")9U", "9U");
                                        if (result == string.Empty)
                                        {
                                            continue;
                                        }

                                        richTextBox2.Invoke(new Action(() =>
                                        {
                                            this.richTextBox2.Text = result.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");
                                        }));

                                        Invoke(new Action(() =>
                                        {
                                            Compare_Master(richTextBox1.Text, richTextBox2.Text);
                                        }));
                                    }
                                }
                            }
                        }
                    }
                    deletedFileTemp();
                   Thread.Sleep(1500);
                }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogWriter.SaveLog("ErrorMessage E001:" + ex.Message);
                LogWriter.SaveLog("Data :" + ex.Data);
            }
        }
            
}


        private async void deletedFileTemp()
        {
            try
            {
                string _dir = Properties.Resources.path_temp;
                string[] files = Directory.GetFiles(_dir);
                int i = 0;
                await Task.Delay(1);
                foreach (string file in files)
                {
                    i++;
                    FileInfo info = new FileInfo(file);
                    if (info.LastAccessTime < DateTime.Now.AddMinutes(-30))
                        info.Delete();
                    if (i > 200)
                        break;
                }
                i = 0;
                files.Reverse();
                foreach (string file in files)
                {
                    i++;
                    FileInfo info = new FileInfo(file);
                    if (info.LastAccessTime < DateTime.Now.AddMinutes(-30))
                        info.Delete();
                    if (i > 200)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        History history;

        private void Compare_Master(string txt_sw, string txt_lb)
        {
            try
            {
                LogWriter.SaveLog("TXT Read :" + txt_sw + ", " + txt_lb);
                //lbTitle.Text;
                history = new History();
                //txt_lb = txt_lb.Replace("O", "0");
                var lb = txt_lb.IndexOf("731TMC");
                // If not found, IndexOf returns -1.
                if (lb == -1)
                {
                    // Return the original string.

                    richTextBox1.Text = string.Empty;
                    richTextBox2.Text = string.Empty;

                    scrollablePictureBoxCamera01.Image = null;
                    scrollablePictureBoxCamera02.Image = null;
                    return;
                }

                int swa = txt_sw.IndexOf("731TMC");
                // If not found, IndexOf returns -1.
                if (swa == -1)
                {
                    // Return the original string.

                    richTextBox1.Text = string.Empty;
                    richTextBox2.Text = string.Empty;

                    scrollablePictureBoxCamera01.Image = null;
                    scrollablePictureBoxCamera02.Image = null;
                    return;
                }
                var txt = txt_lb.Substring(0, lb);
                txt = txt.Replace("O", "0");
                var master_lb = MasterAll.GetMasterALLByLBName(txt);

                bool check = false;
                if (master_lb.Count > 0)
                {
                    foreach (var item in master_lb)
                    {
                        history.master_sw = item.nameSW;
                        history.master_lb = item.nameModel;
                        if (item.nameSW == txt_sw)
                        {
                            check = true;
                            break;
                        }
                    }
                }
                else
                {
                    history.master_sw = "null";
                    history.master_lb = "null";
                }


                if (!check)
                {
                    lbTitle.Text = "NG";
                    lbTitle.ForeColor = Color.White;
                    lbTitle.BackColor = Color.Red;
                    is_Blink_NG = true;
                    serialCommand("NG");
                }
                else
                {
                    lbTitle.Text = "OK";
                    lbTitle.ForeColor = Color.White;
                    lbTitle.BackColor = Color.Green;
                    serialCommand("OK");
                }


                history.name = txtEmployee.Text.Trim();
                history.name_lb = txt_lb;
                history.name_sw = txt_sw;
                history.result = check ? "OK" : "NG";
                history.Save();
                LogWriter.SaveLog("Result :" + history.result);
                isStaetReset = false;
                loadTableHistory();
            }
            catch (Exception ex)
            {
                // Reset 
                LogWriter.SaveLog("Error compare :" + ex.Message);
                scrollablePictureBoxCamera01.Image = null;
                scrollablePictureBoxCamera01.Image = null;
                richTextBox1.Text = string.Empty;
                richTextBox2.Text = string.Empty;

                lbTitle.Text = "Image not match"; // "Image not match";
                serialCommand("NG#");
                isStaetReset = true;
                return;
            }

        }
        private void loadTableHistory()
        {
            var list = History.GetHistory();
            dataGridViewHistory.DataSource = null;
            int i = 0;
            // Reverse the list to display the latest record first
            var data = (from p in list
                        select new
                        {
                            ID = p.id,
                            No = ++i,
                            Employee = p.name,
                            MasterSW = p.master_sw,
                            Software = p.name_sw,
                            Master_Model = p.master_lb,
                            Models = p.name_lb,
                            Results = p.result,
                            Update = p.created_at
                        }).ToList();
            dataGridViewHistory.DataSource = data;
            dataGridViewHistory.Columns[0].Visible = false;
            // 10% of the width of the DataGridView
            dataGridViewHistory.Columns[1].Width = dataGridViewHistory.Width * 10 / 100;
            // last 20% of the width of the DataGridView
            dataGridViewHistory.Columns[dataGridViewHistory.Columns.Count - 1].Width = dataGridViewHistory.Width * 20 / 100;
        }
        private string getFileTemp()
        {
            return Properties.Resources.path_temp + "/" + Guid.NewGuid().ToString() + ".jpg";
        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture_1 != null)
            {
                if (capture_1._isRunning)
                {
                    capture_1.Stop();
                }
                capture_1.Dispose();
            }
            if (capture_2 != null)
            {
                if (capture_2._isRunning)
                {
                    capture_2.Stop();
                }
                capture_2.Dispose();
            }

            if (thread != null)
            {
                thread.Abort();
            }
        }
        SettingModel setting;
        private void masterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setting != null)
            {
                setting.Close();
                setting.Dispose();
            }

            setting = new SettingModel();
            setting.Show();
        }
        Options options;



        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (options != null)
            {
                options.Close();
                options.Dispose();
            }
            options = new Options(this);
            options.Show();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {

            if (is_Blink_NG)
            {
                toggle_blink_ng = !toggle_blink_ng;
                if (toggle_blink_ng)
                {
                    lbTitle.BackColor = Color.Red;
                    lbTitle.ForeColor = Color.White;
                }
                else
                {
                    lbTitle.BackColor = Color.White;
                    lbTitle.ForeColor = Color.Red;
                }
            }
            else if (lbTitle.BackColor != Color.Yellow && isStaetReset)
            {
                lbTitle.BackColor = Color.Yellow;
                lbTitle.ForeColor = Color.Black;
            }
        }

        private void testOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialCommand("OK");
        }

        private void testNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialCommand("NG");
        }

        private void numericUpDownFocus_ValueChanged(object sender, EventArgs e)
        {
            if (!checkBoxAutoFocus.Checked)
            {
                capture_2.SetFocus((int)numericUpDownFocus.Value);
            }
            else
            {
                capture_2.AutoFocus();
            }
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
                entity.ScreenshotMode = false;
                entity.Language = "eng";
                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PageSegMode = selectedPSM;
                ocrEngine.OcrEngineMode = selectedOEM;
                ocrEngine.Language = entity.Language;

                IList<Image> images = entity.ClonedImages;

                string result = ocrEngine.RecognizeText(((List<Image>)images).GetRange(0, 1), entity.Inputfilename);
                return result;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Exclamation A00", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LogWriter.SaveLog("A00 : " + ex.Message);
            }
            return "";
        }
    }
}
