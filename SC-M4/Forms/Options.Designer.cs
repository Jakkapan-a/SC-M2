namespace SC_M4.Forms
{
    partial class Options
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusType = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusImageId = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btCam_1 = new System.Windows.Forms.RadioButton();
            this.btCam_2 = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.contextMenuStripImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.contextMenuStripImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusType,
            this.toolStripStatusImageId});
            this.statusStrip.Location = new System.Drawing.Point(0, 436);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(652, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusType
            // 
            this.toolStripStatusType.Name = "toolStripStatusType";
            this.toolStripStatusType.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusType.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusImageId
            // 
            this.toolStripStatusImageId.Name = "toolStripStatusImageId";
            this.toolStripStatusImageId.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusImageId.Text = "toolStripStatusLabel1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Khaki;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(628, 43);
            this.label1.TabIndex = 2;
            this.label1.Text = "Master Image";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btCam_1
            // 
            this.btCam_1.Appearance = System.Windows.Forms.Appearance.Button;
            this.btCam_1.Location = new System.Drawing.Point(16, 75);
            this.btCam_1.Name = "btCam_1";
            this.btCam_1.Size = new System.Drawing.Size(93, 24);
            this.btCam_1.TabIndex = 3;
            this.btCam_1.Text = "Image Cam 01";
            this.btCam_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btCam_1.UseVisualStyleBackColor = true;
            // 
            // btCam_2
            // 
            this.btCam_2.Appearance = System.Windows.Forms.Appearance.Button;
            this.btCam_2.Location = new System.Drawing.Point(126, 75);
            this.btCam_2.Name = "btCam_2";
            this.btCam_2.Size = new System.Drawing.Size(91, 23);
            this.btCam_2.TabIndex = 3;
            this.btCam_2.Text = "Image Cam 2";
            this.btCam_2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.flowLayoutPanel.ContextMenuStrip = this.contextMenuStripImage;
            this.flowLayoutPanel.Location = new System.Drawing.Point(16, 106);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(624, 294);
            this.flowLayoutPanel.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(565, 406);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // contextMenuStripImage
            // 
            this.contextMenuStripImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStripImage.Name = "contextMenuStripImage";
            this.contextMenuStripImage.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 458);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.btCam_2);
            this.Controls.Add(this.btCam_1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(668, 497);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options_2";
            this.Load += new System.EventHandler(this.Options_2_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStripImage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton btCam_1;
        private System.Windows.Forms.RadioButton btCam_2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusType;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusImageId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripImage;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}