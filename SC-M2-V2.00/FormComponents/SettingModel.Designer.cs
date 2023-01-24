using System;

namespace SC_M2_V2._00.FormComponents
{
    partial class SettingModel
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
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusID_SW = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusID_LB = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.contextMenuStripSetting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameSWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.contextMenuStripSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(560, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "LIST MASTER";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusID_SW,
            this.toolStripStatusID_LB});
            this.statusStrip1.Location = new System.Drawing.Point(0, 357);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusID_SW
            // 
            this.toolStripStatusID_SW.Name = "toolStripStatusID_SW";
            this.toolStripStatusID_SW.Size = new System.Drawing.Size(118, 19);
            this.toolStripStatusID_SW.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusID_LB
            // 
            this.toolStripStatusID_LB.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusID_LB.Name = "toolStripStatusID_LB";
            this.toolStripStatusID_LB.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusID_LB.Text = "toolStripStatusLabel2";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(497, 333);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AllowUserToDeleteRows = false;
            this.dataGridViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.ContextMenuStrip = this.contextMenuStripSetting;
            this.dataGridViewReport.Location = new System.Drawing.Point(15, 50);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.ReadOnly = true;
            this.dataGridViewReport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewReport.RowHeadersVisible = false;
            this.dataGridViewReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReport.Size = new System.Drawing.Size(557, 277);
            this.dataGridViewReport.TabIndex = 3;
            this.dataGridViewReport.SelectionChanged += new System.EventHandler(this.dataGridViewReport_SelectionChanged);
            // 
            // contextMenuStripSetting
            // 
            this.contextMenuStripSetting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.renameSWToolStripMenuItem,
            this.deleteSWToolStripMenuItem,
            this.renameModelToolStripMenuItem,
            this.deleteModelToolStripMenuItem});
            this.contextMenuStripSetting.Name = "contextMenuStripSetting";
            this.contextMenuStripSetting.Size = new System.Drawing.Size(181, 136);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // renameSWToolStripMenuItem
            // 
            this.renameSWToolStripMenuItem.Name = "renameSWToolStripMenuItem";
            this.renameSWToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameSWToolStripMenuItem.Text = "Rename SW";
            this.renameSWToolStripMenuItem.Click += new System.EventHandler(this.renameSWToolStripMenuItem_Click);
            // 
            // deleteSWToolStripMenuItem
            // 
            this.deleteSWToolStripMenuItem.Name = "deleteSWToolStripMenuItem";
            this.deleteSWToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteSWToolStripMenuItem.Text = "Delete SW";
            this.deleteSWToolStripMenuItem.Click += new System.EventHandler(this.deleteSWToolStripMenuItem_Click);
            // 
            // renameModelToolStripMenuItem
            // 
            this.renameModelToolStripMenuItem.Name = "renameModelToolStripMenuItem";
            this.renameModelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renameModelToolStripMenuItem.Text = "Rename Model";
            this.renameModelToolStripMenuItem.Click += new System.EventHandler(this.renameModelToolStripMenuItem_Click);
            // 
            // deleteModelToolStripMenuItem
            // 
            this.deleteModelToolStripMenuItem.Name = "deleteModelToolStripMenuItem";
            this.deleteModelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteModelToolStripMenuItem.Text = "Delete Model";
            this.deleteModelToolStripMenuItem.Click += new System.EventHandler(this.deleteModelToolStripMenuItem_Click);
            // 
            // SettingModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 381);
            this.Controls.Add(this.dataGridViewReport);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Name = "SettingModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.contextMenuStripSetting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSetting;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusID_SW;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusID_LB;
        private System.Windows.Forms.ToolStripMenuItem renameSWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameModelToolStripMenuItem;
    }
}