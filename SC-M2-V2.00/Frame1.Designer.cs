namespace SC_M2_V2._00
{
    partial class Frame1
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
            this.pictureCrop1 = new SC_M2_V2._00.Components.PictureCrop();
            this.timerVideo = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCrop1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureCrop1
            // 
            this.pictureCrop1.Location = new System.Drawing.Point(12, 12);
            this.pictureCrop1.Name = "pictureCrop1";
            this.pictureCrop1.Size = new System.Drawing.Size(913, 556);
            this.pictureCrop1.TabIndex = 0;
            this.pictureCrop1.TabStop = false;
            // 
            // timerVideo
            // 
            this.timerVideo.Tick += new System.EventHandler(this.timerVideo_Tick);
            // 
            // Frame1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 580);
            this.Controls.Add(this.pictureCrop1);
            this.Name = "Frame1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frame1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frame1_FormClosing);
            this.Load += new System.EventHandler(this.Frame1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCrop1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerVideo;
        public Components.PictureCrop pictureCrop1;
    }
}