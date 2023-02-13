namespace SC_M4.Forms
{
    partial class Edit_Items
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edit_Items));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusID_SW = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusID_LB = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddSW = new System.Windows.Forms.Button();
            this.txtInputSW = new System.Windows.Forms.TextBox();
            this.dataGridViewSW = new System.Windows.Forms.DataGridView();
            this.contextMenuStripSW = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddLB = new System.Windows.Forms.Button();
            this.txtInputLB = new System.Windows.Forms.TextBox();
            this.dataGridViewLB = new System.Windows.Forms.DataGridView();
            this.contextMenuStripLB = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSW)).BeginInit();
            this.contextMenuStripSW.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLB)).BeginInit();
            this.contextMenuStripLB.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusID_SW,
            this.toolStripStatusID_LB});
            this.statusStrip.Location = new System.Drawing.Point(0, 394);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(614, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusID_SW
            // 
            this.toolStripStatusID_SW.Name = "toolStripStatusID_SW";
            this.toolStripStatusID_SW.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusID_SW.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusID_LB
            // 
            this.toolStripStatusID_LB.Name = "toolStripStatusID_LB";
            this.toolStripStatusID_LB.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusID_LB.Text = "toolStripStatusLabel2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddSW);
            this.splitContainer1.Panel1.Controls.Add(this.txtInputSW);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewSW);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddLB);
            this.splitContainer1.Panel2.Controls.Add(this.txtInputLB);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewLB);
            this.splitContainer1.Size = new System.Drawing.Size(614, 394);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "List Version Software In ECU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddSW
            // 
            this.btnAddSW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSW.Location = new System.Drawing.Point(223, 359);
            this.btnAddSW.Name = "btnAddSW";
            this.btnAddSW.Size = new System.Drawing.Size(75, 23);
            this.btnAddSW.TabIndex = 2;
            this.btnAddSW.Text = "Add";
            this.btnAddSW.UseVisualStyleBackColor = true;
            this.btnAddSW.Click += new System.EventHandler(this.btnAddSW_Click);
            // 
            // txtInputSW
            // 
            this.txtInputSW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputSW.Location = new System.Drawing.Point(12, 360);
            this.txtInputSW.Name = "txtInputSW";
            this.txtInputSW.Size = new System.Drawing.Size(205, 20);
            this.txtInputSW.TabIndex = 1;
            // 
            // dataGridViewSW
            // 
            this.dataGridViewSW.AllowUserToAddRows = false;
            this.dataGridViewSW.AllowUserToDeleteRows = false;
            this.dataGridViewSW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSW.ContextMenuStrip = this.contextMenuStripSW;
            this.dataGridViewSW.Location = new System.Drawing.Point(12, 45);
            this.dataGridViewSW.Name = "dataGridViewSW";
            this.dataGridViewSW.RowHeadersVisible = false;
            this.dataGridViewSW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSW.Size = new System.Drawing.Size(286, 309);
            this.dataGridViewSW.TabIndex = 0;
            this.dataGridViewSW.SelectionChanged += new System.EventHandler(this.dataGridViewSW_SelectionChanged);
            // 
            // contextMenuStripSW
            // 
            this.contextMenuStripSW.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripSW.Name = "contextMenuStripSW";
            this.contextMenuStripSW.Size = new System.Drawing.Size(118, 48);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "List Models In Label";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddLB
            // 
            this.btnAddLB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLB.Location = new System.Drawing.Point(230, 360);
            this.btnAddLB.Name = "btnAddLB";
            this.btnAddLB.Size = new System.Drawing.Size(75, 23);
            this.btnAddLB.TabIndex = 2;
            this.btnAddLB.Text = "Add";
            this.btnAddLB.UseVisualStyleBackColor = true;
            this.btnAddLB.Click += new System.EventHandler(this.btnAddLB_Click);
            // 
            // txtInputLB
            // 
            this.txtInputLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputLB.Location = new System.Drawing.Point(6, 360);
            this.txtInputLB.Name = "txtInputLB";
            this.txtInputLB.Size = new System.Drawing.Size(209, 20);
            this.txtInputLB.TabIndex = 1;
            // 
            // dataGridViewLB
            // 
            this.dataGridViewLB.AllowUserToAddRows = false;
            this.dataGridViewLB.AllowUserToDeleteRows = false;
            this.dataGridViewLB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLB.ContextMenuStrip = this.contextMenuStripLB;
            this.dataGridViewLB.Location = new System.Drawing.Point(3, 45);
            this.dataGridViewLB.Name = "dataGridViewLB";
            this.dataGridViewLB.RowHeadersVisible = false;
            this.dataGridViewLB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLB.Size = new System.Drawing.Size(293, 309);
            this.dataGridViewLB.TabIndex = 0;
            this.dataGridViewLB.SelectionChanged += new System.EventHandler(this.dataGridViewLB_SelectionChanged);
            // 
            // contextMenuStripLB
            // 
            this.contextMenuStripLB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.contextMenuStripLB.Name = "contextMenuStripLB";
            this.contextMenuStripLB.Size = new System.Drawing.Size(118, 48);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // Edit_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 416);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Edit_Items";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit_Items";
            this.Load += new System.EventHandler(this.Add_Item_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSW)).EndInit();
            this.contextMenuStripSW.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLB)).EndInit();
            this.contextMenuStripLB.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddSW;
        private System.Windows.Forms.TextBox txtInputSW;
        private System.Windows.Forms.DataGridView dataGridViewSW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddLB;
        private System.Windows.Forms.TextBox txtInputLB;
        private System.Windows.Forms.DataGridView dataGridViewLB;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusID_SW;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusID_LB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSW;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLB;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
    }
}