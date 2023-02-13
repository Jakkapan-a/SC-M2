namespace SC_M4.Forms
{
    partial class Crop_Image
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Crop_Image));
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerVideo = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.pictureCrop = new Controls.ScrollablePictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCrop)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pictureCrop);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 347);
            this.panel1.TabIndex = 0;
            // 
            // timerVideo
            // 
            this.timerVideo.Tick += new System.EventHandler(this.timerVideo_Tick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(665, 370);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pictureCrop
            // 
            this.pictureCrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureCrop.Location = new System.Drawing.Point(0, 0);
            this.pictureCrop.Name = "pictureCrop";
            this.pictureCrop.SegmentedRegions = null;
            this.pictureCrop.Size = new System.Drawing.Size(727, 347);
            this.pictureCrop.TabIndex = 1;
            this.pictureCrop.TabStop = false;
            // 
            // Crop_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 405);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Crop_Image";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crop_Image";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Crop_Image_FormClosing);
            this.Load += new System.EventHandler(this.Crop_Image_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCrop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerVideo;
        private System.Windows.Forms.Button btnSave;
        private Controls.ScrollablePictureBox pictureCrop;
    }
}