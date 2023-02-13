namespace SC_M4
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
            this.menuStripHome = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripHome = new System.Windows.Forms.StatusStrip();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btStartStop = new System.Windows.Forms.Button();
            this.lbTitle = new System.Windows.Forms.Label();
            this.splitContainerHomeMain = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainerHomeBody = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxCamera01 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelChild01 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.scrollablePictureBoxCamera01 = new SC_M4.Controls.ScrollablePictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxCamera02 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelChild02 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.scrollablePictureBoxCamera02 = new SC_M4.Controls.ScrollablePictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxCamera1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCOMPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.comboBoxCamera2 = new System.Windows.Forms.ComboBox();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStripHome.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHomeMain)).BeginInit();
            this.splitContainerHomeMain.Panel1.SuspendLayout();
            this.splitContainerHomeMain.Panel2.SuspendLayout();
            this.splitContainerHomeMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHomeBody)).BeginInit();
            this.splitContainerHomeBody.Panel1.SuspendLayout();
            this.splitContainerHomeBody.Panel2.SuspendLayout();
            this.splitContainerHomeBody.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera01)).BeginInit();
            this.tableLayoutPanelChild01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollablePictureBoxCamera01)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera02)).BeginInit();
            this.tableLayoutPanelChild02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrollablePictureBoxCamera02)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripHome
            // 
            this.menuStripHome.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.menuStripHome.Location = new System.Drawing.Point(0, 0);
            this.menuStripHome.Name = "menuStripHome";
            this.menuStripHome.Size = new System.Drawing.Size(946, 24);
            this.menuStripHome.TabIndex = 0;
            this.menuStripHome.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterListToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // masterListToolStripMenuItem
            // 
            this.masterListToolStripMenuItem.Name = "masterListToolStripMenuItem";
            this.masterListToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.masterListToolStripMenuItem.Text = "Master List";
            this.masterListToolStripMenuItem.Click += new System.EventHandler(this.masterListToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // statusStripHome
            // 
            this.statusStripHome.Location = new System.Drawing.Point(0, 571);
            this.statusStripHome.Name = "statusStripHome";
            this.statusStripHome.Size = new System.Drawing.Size(946, 22);
            this.statusStripHome.TabIndex = 1;
            this.statusStripHome.Text = "statusStrip1";
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.Controls.Add(this.btStartStop);
            this.panelHeader.Controls.Add(this.lbTitle);
            this.panelHeader.Location = new System.Drawing.Point(12, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(922, 67);
            this.panelHeader.TabIndex = 2;
            // 
            // btStartStop
            // 
            this.btStartStop.Location = new System.Drawing.Point(7, 12);
            this.btStartStop.Name = "btStartStop";
            this.btStartStop.Size = new System.Drawing.Size(92, 45);
            this.btStartStop.TabIndex = 8;
            this.btStartStop.Text = "START";
            this.btStartStop.UseVisualStyleBackColor = true;
            this.btStartStop.Click += new System.EventHandler(this.btStartStop_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitle.BackColor = System.Drawing.Color.Yellow;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(142, 4);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(639, 59);
            this.lbTitle.TabIndex = 7;
            this.lbTitle.Text = "-------------------------";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainerHomeMain
            // 
            this.splitContainerHomeMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerHomeMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerHomeMain.Location = new System.Drawing.Point(12, 97);
            this.splitContainerHomeMain.Name = "splitContainerHomeMain";
            this.splitContainerHomeMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerHomeMain.Panel1
            // 
            this.splitContainerHomeMain.Panel1.Controls.Add(this.groupBox2);
            this.splitContainerHomeMain.Panel1.Controls.Add(this.splitContainerHomeBody);
            this.splitContainerHomeMain.Panel1.Controls.Add(this.groupBox1);
            this.splitContainerHomeMain.Panel1.Margin = new System.Windows.Forms.Padding(5);
            // 
            // splitContainerHomeMain.Panel2
            // 
            this.splitContainerHomeMain.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainerHomeMain.Panel2.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainerHomeMain.Size = new System.Drawing.Size(922, 471);
            this.splitContainerHomeMain.SplitterDistance = 344;
            this.splitContainerHomeMain.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(690, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "USER INPUT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Employee :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 70);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 20);
            this.textBox1.TabIndex = 0;
            // 
            // splitContainerHomeBody
            // 
            this.splitContainerHomeBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerHomeBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerHomeBody.Location = new System.Drawing.Point(3, 3);
            this.splitContainerHomeBody.Name = "splitContainerHomeBody";
            // 
            // splitContainerHomeBody.Panel1
            // 
            this.splitContainerHomeBody.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainerHomeBody.Panel2
            // 
            this.splitContainerHomeBody.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainerHomeBody.Size = new System.Drawing.Size(679, 323);
            this.splitContainerHomeBody.SplitterDistance = 345;
            this.splitContainerHomeBody.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxCamera01, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelChild01, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.1769F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.8231F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(337, 315);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBoxCamera01
            // 
            this.pictureBoxCamera01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCamera01.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxCamera01.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxCamera01.Name = "pictureBoxCamera01";
            this.pictureBoxCamera01.Size = new System.Drawing.Size(331, 193);
            this.pictureBoxCamera01.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera01.TabIndex = 0;
            this.pictureBoxCamera01.TabStop = false;
            // 
            // tableLayoutPanelChild01
            // 
            this.tableLayoutPanelChild01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelChild01.ColumnCount = 2;
            this.tableLayoutPanelChild01.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChild01.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChild01.Controls.Add(this.richTextBox1, 1, 0);
            this.tableLayoutPanelChild01.Controls.Add(this.scrollablePictureBoxCamera01, 0, 0);
            this.tableLayoutPanelChild01.Location = new System.Drawing.Point(3, 202);
            this.tableLayoutPanelChild01.Name = "tableLayoutPanelChild01";
            this.tableLayoutPanelChild01.RowCount = 1;
            this.tableLayoutPanelChild01.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChild01.Size = new System.Drawing.Size(331, 110);
            this.tableLayoutPanelChild01.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(168, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(160, 104);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // scrollablePictureBoxCamera01
            // 
            this.scrollablePictureBoxCamera01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollablePictureBoxCamera01.Location = new System.Drawing.Point(3, 3);
            this.scrollablePictureBoxCamera01.Name = "scrollablePictureBoxCamera01";
            this.scrollablePictureBoxCamera01.SegmentedRegions = null;
            this.scrollablePictureBoxCamera01.Size = new System.Drawing.Size(159, 104);
            this.scrollablePictureBoxCamera01.TabIndex = 1;
            this.scrollablePictureBoxCamera01.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBoxCamera02, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelChild02, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.1769F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.8231F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(322, 312);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pictureBoxCamera02
            // 
            this.pictureBoxCamera02.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCamera02.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxCamera02.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxCamera02.Name = "pictureBoxCamera02";
            this.pictureBoxCamera02.Size = new System.Drawing.Size(316, 191);
            this.pictureBoxCamera02.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera02.TabIndex = 0;
            this.pictureBoxCamera02.TabStop = false;
            // 
            // tableLayoutPanelChild02
            // 
            this.tableLayoutPanelChild02.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelChild02.ColumnCount = 2;
            this.tableLayoutPanelChild02.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChild02.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChild02.Controls.Add(this.richTextBox2, 1, 0);
            this.tableLayoutPanelChild02.Controls.Add(this.scrollablePictureBoxCamera02, 0, 0);
            this.tableLayoutPanelChild02.Location = new System.Drawing.Point(3, 200);
            this.tableLayoutPanelChild02.Name = "tableLayoutPanelChild02";
            this.tableLayoutPanelChild02.RowCount = 1;
            this.tableLayoutPanelChild02.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelChild02.Size = new System.Drawing.Size(316, 109);
            this.tableLayoutPanelChild02.TabIndex = 1;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(161, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(152, 103);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // scrollablePictureBoxCamera02
            // 
            this.scrollablePictureBoxCamera02.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollablePictureBoxCamera02.Location = new System.Drawing.Point(3, 3);
            this.scrollablePictureBoxCamera02.Name = "scrollablePictureBoxCamera02";
            this.scrollablePictureBoxCamera02.SegmentedRegions = null;
            this.scrollablePictureBoxCamera02.Size = new System.Drawing.Size(152, 103);
            this.scrollablePictureBoxCamera02.TabIndex = 1;
            this.scrollablePictureBoxCamera02.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxCamera1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxCOMPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btRefresh);
            this.groupBox1.Controls.Add(this.btConnect);
            this.groupBox1.Controls.Add(this.comboBoxCamera2);
            this.groupBox1.Controls.Add(this.comboBoxBaud);
            this.groupBox1.Location = new System.Drawing.Point(690, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 211);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SETTING";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "COM Port :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Drive Camera 1 :";
            // 
            // comboBoxCamera1
            // 
            this.comboBoxCamera1.FormattingEnabled = true;
            this.comboBoxCamera1.Location = new System.Drawing.Point(93, 28);
            this.comboBoxCamera1.Name = "comboBoxCamera1";
            this.comboBoxCamera1.Size = new System.Drawing.Size(132, 21);
            this.comboBoxCamera1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Baud :";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Location = new System.Drawing.Point(93, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 12);
            this.label1.TabIndex = 10;
            // 
            // comboBoxCOMPort
            // 
            this.comboBoxCOMPort.FormattingEnabled = true;
            this.comboBoxCOMPort.Location = new System.Drawing.Point(94, 148);
            this.comboBoxCOMPort.Name = "comboBoxCOMPort";
            this.comboBoxCOMPort.Size = new System.Drawing.Size(132, 21);
            this.comboBoxCOMPort.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Drive Camera 2 :";
            // 
            // btRefresh
            // 
            this.btRefresh.Image = global::SC_M4.Properties.Resources._refresh_16;
            this.btRefresh.Location = new System.Drawing.Point(5, 174);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(31, 25);
            this.btRefresh.TabIndex = 11;
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(150, 174);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 25);
            this.btConnect.TabIndex = 11;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            // 
            // comboBoxCamera2
            // 
            this.comboBoxCamera2.FormattingEnabled = true;
            this.comboBoxCamera2.Location = new System.Drawing.Point(93, 55);
            this.comboBoxCamera2.Name = "comboBoxCamera2";
            this.comboBoxCamera2.Size = new System.Drawing.Size(132, 21);
            this.comboBoxCamera2.TabIndex = 8;
            // 
            // comboBoxBaud
            // 
            this.comboBoxBaud.FormattingEnabled = true;
            this.comboBoxBaud.Location = new System.Drawing.Point(94, 121);
            this.comboBoxBaud.Name = "comboBoxBaud";
            this.comboBoxBaud.Size = new System.Drawing.Size(132, 21);
            this.comboBoxBaud.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(914, 115);
            this.dataGridView1.TabIndex = 0;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 593);
            this.Controls.Add(this.splitContainerHomeMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStripHome);
            this.Controls.Add(this.menuStripHome);
            this.MainMenuStrip = this.menuStripHome;
            this.Name = "Home";
            this.Text = "SC-M4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            this.menuStripHome.ResumeLayout(false);
            this.menuStripHome.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.splitContainerHomeMain.Panel1.ResumeLayout(false);
            this.splitContainerHomeMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHomeMain)).EndInit();
            this.splitContainerHomeMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainerHomeBody.Panel1.ResumeLayout(false);
            this.splitContainerHomeBody.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHomeBody)).EndInit();
            this.splitContainerHomeBody.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera01)).EndInit();
            this.tableLayoutPanelChild01.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scrollablePictureBoxCamera01)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera02)).EndInit();
            this.tableLayoutPanelChild02.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scrollablePictureBoxCamera02)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripHome;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripHome;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Button btStartStop;
        private System.Windows.Forms.SplitContainer splitContainerHomeMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainerHomeBody;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxCamera1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCOMPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.ComboBox comboBoxCamera2;
        private System.Windows.Forms.ComboBox comboBoxBaud;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBoxCamera01;
        private System.Windows.Forms.PictureBox pictureBoxCamera02;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelChild01;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelChild02;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private Controls.ScrollablePictureBox scrollablePictureBoxCamera01;
        private Controls.ScrollablePictureBox scrollablePictureBoxCamera02;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Button btRefresh;
    }
}

