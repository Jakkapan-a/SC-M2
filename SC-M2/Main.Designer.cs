namespace SC_M2
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelConnectControl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusData = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelData = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbQrcode = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbOutput = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCameraDevice = new System.Windows.Forms.ComboBox();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.timerVideo = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timerCouter = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxC = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxC)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConnectControl,
            this.toolStripStatusData,
            this.toolStripStatusLabelData});
            this.statusStrip1.Location = new System.Drawing.Point(0, 569);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(913, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelConnectControl
            // 
            this.toolStripStatusLabelConnectControl.Name = "toolStripStatusLabelConnectControl";
            this.toolStripStatusLabelConnectControl.Size = new System.Drawing.Size(118, 19);
            this.toolStripStatusLabelConnectControl.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusData
            // 
            this.toolStripStatusData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusData.Name = "toolStripStatusData";
            this.toolStripStatusData.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusData.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelData
            // 
            this.toolStripStatusLabelData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelData.Name = "toolStripStatusLabelData";
            this.toolStripStatusLabelData.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusLabelData.Text = "toolStripStatusLabel1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.connectControllerToolStripMenuItem,
            this.connectCameraToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 70);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Image = global::SC_M2.Properties.Resources.settings_32;
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // connectControllerToolStripMenuItem
            // 
            this.connectControllerToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.connectControllerToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.connectControllerToolStripMenuItem.Image = global::SC_M2.Properties.Resources.USB_Connector_32;
            this.connectControllerToolStripMenuItem.Name = "connectControllerToolStripMenuItem";
            this.connectControllerToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.connectControllerToolStripMenuItem.Text = "Connect Controller";
            this.connectControllerToolStripMenuItem.Click += new System.EventHandler(this.connectControllerToolStripMenuItem_Click);
            // 
            // connectCameraToolStripMenuItem
            // 
            this.connectCameraToolStripMenuItem.Image = global::SC_M2.Properties.Resources.camera_32;
            this.connectCameraToolStripMenuItem.Name = "connectCameraToolStripMenuItem";
            this.connectCameraToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.connectCameraToolStripMenuItem.Text = "Connect Camera";
            this.connectCameraToolStripMenuItem.Click += new System.EventHandler(this.connectCameraToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbQrcode);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Location = new System.Drawing.Point(593, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 126);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INPUT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "QR Code :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name :";
            // 
            // tbQrcode
            // 
            this.tbQrcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQrcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQrcode.Location = new System.Drawing.Point(118, 81);
            this.tbQrcode.Name = "tbQrcode";
            this.tbQrcode.Size = new System.Drawing.Size(184, 22);
            this.tbQrcode.TabIndex = 1;
            this.tbQrcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this._KeyDown);
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName.Location = new System.Drawing.Point(118, 40);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(184, 22);
            this.tbName.TabIndex = 0;
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this._KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbOutput);
            this.groupBox2.Location = new System.Drawing.Point(593, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(308, 152);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "JUDGEMENT";
            // 
            // lbOutput
            // 
            this.lbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOutput.Location = new System.Drawing.Point(10, 23);
            this.lbOutput.Name = "lbOutput";
            this.lbOutput.Size = new System.Drawing.Size(288, 119);
            this.lbOutput.TabIndex = 0;
            this.lbOutput.Text = "Wait..";
            this.lbOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btConnect);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboBoxCameraDevice);
            this.groupBox3.Controls.Add(this.comboBoxBaudRate);
            this.groupBox3.Controls.Add(this.comboBoxComPort);
            this.groupBox3.Location = new System.Drawing.Point(593, 412);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(308, 156);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Connection";
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(177, 126);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(121, 23);
            this.btConnect.TabIndex = 3;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Camera Device :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Baud Rate :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Com Port :";
            // 
            // comboBoxCameraDevice
            // 
            this.comboBoxCameraDevice.FormattingEnabled = true;
            this.comboBoxCameraDevice.Location = new System.Drawing.Point(118, 93);
            this.comboBoxCameraDevice.Name = "comboBoxCameraDevice";
            this.comboBoxCameraDevice.Size = new System.Drawing.Size(180, 21);
            this.comboBoxCameraDevice.TabIndex = 2;
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Location = new System.Drawing.Point(118, 62);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(180, 21);
            this.comboBoxBaudRate.TabIndex = 1;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(118, 29);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(180, 21);
            this.comboBoxComPort.TabIndex = 0;
            // 
            // timerVideo
            // 
            this.timerVideo.Interval = 45;
            this.timerVideo.Tick += new System.EventHandler(this.timerVideo_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Location = new System.Drawing.Point(12, 441);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(575, 120);
            this.dataGridView1.TabIndex = 5;
            // 
            // timerCouter
            // 
            this.timerCouter.Interval = 2000;
            this.timerCouter.Tick += new System.EventHandler(this.timerCouter_Tick);
            // 
            // pictureBoxC
            // 
            this.pictureBoxC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxC.BackColor = System.Drawing.Color.DimGray;
            this.pictureBoxC.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxC.Name = "pictureBoxC";
            this.pictureBoxC.Size = new System.Drawing.Size(575, 424);
            this.pictureBoxC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxC.TabIndex = 1;
            this.pictureBoxC.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 593);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBoxC);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(929, 632);
            this.Name = "Main";
            this.Text = "731TMC ECU INSPECTION SOFTWARE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.PictureBox pictureBoxC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbQrcode;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbOutput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.ComboBox comboBoxCameraDevice;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Timer timerVideo;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnectControl;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timerCouter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusData;
        private System.Windows.Forms.ToolStripMenuItem connectControllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelData;
    }
}

