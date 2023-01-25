namespace SC_M2_V2_00
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.statusStripStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusDrive = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusConnectionCamera = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusConnectSerialPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterModelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerHome = new System.Windows.Forms.SplitContainer();
            this.pictureBoxCamera1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lbCamTitle_1 = new System.Windows.Forms.Label();
            this.pictureBoxCamera2 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.lbCamTitle_2 = new System.Windows.Forms.Label();
            this.panelTable = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmployee = new System.Windows.Forms.TextBox();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnOCR2 = new System.Windows.Forms.Button();
            this.btnOCR = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.timerVideo1 = new System.Windows.Forms.Timer(this.components);
            this.timerVideo2 = new System.Windows.Forms.Timer(this.components);
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.timerStartStop = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorkerOcr = new System.ComponentModel.BackgroundWorker();
            this.timerRunOCR = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxCamDetect1 = new OCR1.Controls.ScrollablePictureBox();
            this.pictureBoxCamDetect2 = new OCR1.Controls.ScrollablePictureBox();
            this.statusStripStatus.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHome)).BeginInit();
            this.splitContainerHome.Panel1.SuspendLayout();
            this.splitContainerHome.Panel2.SuspendLayout();
            this.splitContainerHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera2)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamDetect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamDetect2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStripStatus
            // 
            this.statusStripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusDrive,
            this.toolStripStatusConnectionCamera,
            this.toolStripStatusConnectSerialPort,
            this.toolStripStatusLabel4});
            this.statusStripStatus.Location = new System.Drawing.Point(0, 569);
            this.statusStripStatus.Name = "statusStripStatus";
            this.statusStripStatus.Size = new System.Drawing.Size(913, 24);
            this.statusStripStatus.TabIndex = 2;
            this.statusStripStatus.Text = "statusStrip1";
            // 
            // toolStripStatusDrive
            // 
            this.toolStripStatusDrive.Name = "toolStripStatusDrive";
            this.toolStripStatusDrive.Size = new System.Drawing.Size(118, 19);
            this.toolStripStatusDrive.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusConnectionCamera
            // 
            this.toolStripStatusConnectionCamera.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusConnectionCamera.Name = "toolStripStatusConnectionCamera";
            this.toolStripStatusConnectionCamera.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusConnectionCamera.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusConnectSerialPort
            // 
            this.toolStripStatusConnectSerialPort.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusConnectSerialPort.Name = "toolStripStatusConnectSerialPort";
            this.toolStripStatusConnectSerialPort.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusConnectSerialPort.Text = "toolStripStatusLabel3";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(913, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.textTestToolStripMenuItem});
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.aToolStripMenuItem.Text = "Command";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.startToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // textTestToolStripMenuItem
            // 
            this.textTestToolStripMenuItem.Name = "textTestToolStripMenuItem";
            this.textTestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.textTestToolStripMenuItem.Text = "Text test";
            this.textTestToolStripMenuItem.Click += new System.EventHandler(this.textTestToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conectionsToolStripMenuItem,
            this.masterModelsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // conectionsToolStripMenuItem
            // 
            this.conectionsToolStripMenuItem.Name = "conectionsToolStripMenuItem";
            this.conectionsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.conectionsToolStripMenuItem.Text = "Connections.";
            this.conectionsToolStripMenuItem.Click += new System.EventHandler(this.conectionsToolStripMenuItem_Click);
            // 
            // masterModelsToolStripMenuItem
            // 
            this.masterModelsToolStripMenuItem.Name = "masterModelsToolStripMenuItem";
            this.masterModelsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.masterModelsToolStripMenuItem.Text = "Master List";
            this.masterModelsToolStripMenuItem.Click += new System.EventHandler(this.masterModelsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.optionsToolStripMenuItem.Text = "Options.";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // splitContainerHome
            // 
            this.splitContainerHome.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerHome.Location = new System.Drawing.Point(13, 67);
            this.splitContainerHome.Name = "splitContainerHome";
            // 
            // splitContainerHome.Panel1
            // 
            this.splitContainerHome.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainerHome.Panel1.Controls.Add(this.pictureBoxCamera1);
            this.splitContainerHome.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainerHome.Panel1.Controls.Add(this.lbCamTitle_1);
            // 
            // splitContainerHome.Panel2
            // 
            this.splitContainerHome.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainerHome.Panel2.Controls.Add(this.pictureBoxCamera2);
            this.splitContainerHome.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainerHome.Panel2.Controls.Add(this.lbCamTitle_2);
            this.splitContainerHome.Size = new System.Drawing.Size(887, 339);
            this.splitContainerHome.SplitterDistance = 439;
            this.splitContainerHome.TabIndex = 5;
            // 
            // pictureBoxCamera1
            // 
            this.pictureBoxCamera1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCamera1.Location = new System.Drawing.Point(0, 35);
            this.pictureBoxCamera1.Name = "pictureBoxCamera1";
            this.pictureBoxCamera1.Size = new System.Drawing.Size(436, 188);
            this.pictureBoxCamera1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera1.TabIndex = 2;
            this.pictureBoxCamera1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxCamDetect1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 223);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 116);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(222, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(214, 110);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // lbCamTitle_1
            // 
            this.lbCamTitle_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbCamTitle_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCamTitle_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbCamTitle_1.Location = new System.Drawing.Point(0, 0);
            this.lbCamTitle_1.Name = "lbCamTitle_1";
            this.lbCamTitle_1.Size = new System.Drawing.Size(439, 35);
            this.lbCamTitle_1.TabIndex = 0;
            this.lbCamTitle_1.Text = "CAMERA 1";
            this.lbCamTitle_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxCamera2
            // 
            this.pictureBoxCamera2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCamera2.Location = new System.Drawing.Point(0, 35);
            this.pictureBoxCamera2.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.pictureBoxCamera2.Name = "pictureBoxCamera2";
            this.pictureBoxCamera2.Size = new System.Drawing.Size(434, 188);
            this.pictureBoxCamera2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera2.TabIndex = 2;
            this.pictureBoxCamera2.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.77478F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.22522F));
            this.tableLayoutPanel2.Controls.Add(this.richTextBox2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBoxCamDetect2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 223);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(444, 116);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(224, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(217, 110);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            // 
            // lbCamTitle_2
            // 
            this.lbCamTitle_2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbCamTitle_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCamTitle_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbCamTitle_2.Location = new System.Drawing.Point(0, 0);
            this.lbCamTitle_2.Name = "lbCamTitle_2";
            this.lbCamTitle_2.Size = new System.Drawing.Size(444, 35);
            this.lbCamTitle_2.TabIndex = 0;
            this.lbCamTitle_2.Text = "CAMERA 2";
            this.lbCamTitle_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTable
            // 
            this.panelTable.Controls.Add(this.label3);
            this.panelTable.Controls.Add(this.txtEmployee);
            this.panelTable.Controls.Add(this.dataGridViewHistory);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTable.Location = new System.Drawing.Point(0, 441);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(913, 128);
            this.panelTable.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(459, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Employee :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEmployee
            // 
            this.txtEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmployee.Location = new System.Drawing.Point(680, 6);
            this.txtEmployee.Name = "txtEmployee";
            this.txtEmployee.Size = new System.Drawing.Size(210, 20);
            this.txtEmployee.TabIndex = 9;
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToAddRows = false;
            this.dataGridViewHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Location = new System.Drawing.Point(13, 32);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.RowHeadersVisible = false;
            this.dataGridViewHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistory.Size = new System.Drawing.Size(884, 93);
            this.dataGridViewHistory.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnOCR2);
            this.panelMain.Controls.Add(this.btnOCR);
            this.panelMain.Controls.Add(this.lbTitle);
            this.panelMain.Controls.Add(this.splitContainerHome);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 24);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.panelMain.Size = new System.Drawing.Size(913, 417);
            this.panelMain.TabIndex = 7;
            // 
            // btnOCR2
            // 
            this.btnOCR2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOCR2.Location = new System.Drawing.Point(825, 33);
            this.btnOCR2.Name = "btnOCR2";
            this.btnOCR2.Size = new System.Drawing.Size(75, 23);
            this.btnOCR2.TabIndex = 8;
            this.btnOCR2.Text = "OCR 2";
            this.btnOCR2.UseVisualStyleBackColor = true;
            this.btnOCR2.Click += new System.EventHandler(this.btnOCR2_Click);
            // 
            // btnOCR
            // 
            this.btnOCR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOCR.Location = new System.Drawing.Point(825, 9);
            this.btnOCR.Name = "btnOCR";
            this.btnOCR.Size = new System.Drawing.Size(75, 23);
            this.btnOCR.TabIndex = 7;
            this.btnOCR.Text = "OCR";
            this.btnOCR.UseVisualStyleBackColor = true;
            this.btnOCR.Click += new System.EventHandler(this.btnOCR_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(13, 10);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(884, 47);
            this.lbTitle.TabIndex = 6;
            this.lbTitle.Text = "-------------------------";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerMain
            // 
            this.timerMain.Interval = 900;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // timerVideo1
            // 
            this.timerVideo1.Interval = 500;
            this.timerVideo1.Tick += new System.EventHandler(this.timerVideo1_Tick);
            // 
            // timerVideo2
            // 
            this.timerVideo2.Tick += new System.EventHandler(this.timerVideo2_Tick);
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // timerStartStop
            // 
            this.timerStartStop.Interval = 10;
            // 
            // backgroundWorkerOcr
            // 
            this.backgroundWorkerOcr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerOcr_DoWork);
            this.backgroundWorkerOcr.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerOcr_ProgressChanged);
            this.backgroundWorkerOcr.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerOcr_RunWorkerCompleted);
            // 
            // pictureBoxCamDetect1
            // 
            this.pictureBoxCamDetect1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCamDetect1.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxCamDetect1.Name = "pictureBoxCamDetect1";
            this.pictureBoxCamDetect1.SegmentedRegions = null;
            this.pictureBoxCamDetect1.Size = new System.Drawing.Size(213, 110);
            this.pictureBoxCamDetect1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamDetect1.TabIndex = 1;
            this.pictureBoxCamDetect1.TabStop = false;
            // 
            // pictureBoxCamDetect2
            // 
            this.pictureBoxCamDetect2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCamDetect2.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxCamDetect2.Name = "pictureBoxCamDetect2";
            this.pictureBoxCamDetect2.SegmentedRegions = null;
            this.pictureBoxCamDetect2.Size = new System.Drawing.Size(215, 110);
            this.pictureBoxCamDetect2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamDetect2.TabIndex = 3;
            this.pictureBoxCamDetect2.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 593);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.statusStripStatus);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(929, 632);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "731TMC ECU INSPECTION SOFTWARE V2.10";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            this.statusStripStatus.ResumeLayout(false);
            this.statusStripStatus.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainerHome.Panel1.ResumeLayout(false);
            this.splitContainerHome.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHome)).EndInit();
            this.splitContainerHome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera2)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelTable.ResumeLayout(false);
            this.panelTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamDetect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamDetect2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStripStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerHome;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.Label lbCamTitle_1;
        private System.Windows.Forms.Label lbCamTitle_2;
        private System.Windows.Forms.PictureBox pictureBoxCamera1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBoxCamera2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Timer timerVideo1;
        private System.Windows.Forms.Timer timerVideo2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusConnectionCamera;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusConnectSerialPort;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conectionsToolStripMenuItem;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ToolStripMenuItem masterModelsToolStripMenuItem;
        private System.Windows.Forms.Timer timerStartStop;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatusDrive;
        private System.ComponentModel.BackgroundWorker backgroundWorkerOcr;
        private OCR1.Controls.ScrollablePictureBox pictureBoxCamDetect1;
        private System.Windows.Forms.Timer timerRunOCR;
        private System.Windows.Forms.Button btnOCR;
        private System.Windows.Forms.Button btnOCR2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private OCR1.Controls.ScrollablePictureBox pictureBoxCamDetect2;
        private System.Windows.Forms.ToolStripMenuItem textTestToolStripMenuItem;
        private System.Windows.Forms.TextBox txtEmployee;
        private System.Windows.Forms.Label label3;
    }
}

