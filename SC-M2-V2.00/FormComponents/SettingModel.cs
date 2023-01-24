using SC_M2_V2._00.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M2_V2._00.FormComponents
{
    public partial class SettingModel : Form
    {
        public SettingModel()
        {
            InitializeComponent();
        }

        public int id_sw = -1;
        public int id_lb = -1;
        Edit_Items _Items;
        Rename rename;
        private void Setting_Load(object sender, EventArgs e)
        {
            loadTable();
            // Clear status bar
            foreach (ToolStripItem item in statusStrip1.Items)
            {
                if (item is ToolStripStatusLabel)
                {
                    item.Text = "";
                }
            }
        }

        public void loadTable(){

            var all = MasterAll.GetMasterAll();

            dataGridViewReport.DataSource = null;

            int i = 0;

            var data = (from a in all
                        select new
                        {
                            ID_SW = a.id_sw,
                            ID_LB = a.id_lb,
                            No = ++i,
                            Name_SW = a.nameSW,
                            Name_Model = a.nameModel,
                            Updated = a.updated_at
                        }).ToList();

            dataGridViewReport.DataSource = data;
            dataGridViewReport.Columns[0].Visible = false;
            dataGridViewReport.Columns[1].Visible = false;
            dataGridViewReport.Columns[2].Width = dataGridViewReport.Width / 10;


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(_Items != null)
            {
                _Items.Close();
                _Items.Dispose();
                _Items= null;
            }
            _Items = new Edit_Items();
            _Items.Show();
        }
        private void dataGridViewReport_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewReport.SelectedRows.Count > 0)
            {
                id_sw = int.Parse(dataGridViewReport.SelectedRows[0].Cells[0].Value.ToString());
                id_lb = int.Parse(dataGridViewReport.SelectedRows[0].Cells[1].Value.ToString());
                toolStripStatusID_SW.Text = "SW :"+id_sw.ToString();
                toolStripStatusID_LB.Text = "LB :"+id_lb.ToString();
            }
        }

        private void renameSWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(rename != null)
            {
                rename.Close();
                rename.Dispose();
                rename = null;
            }
            if(id_sw == 0)
            {
                MessageBox.Show("Can't rename this software name", "Rename Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            rename = new Rename(this,0);
            rename.Show();
        }

        private void renameModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(rename != null)
            {
                rename.Close();
                rename.Dispose();
                rename = null;
            }
            if(id_lb == 0)
            {
                MessageBox.Show("Can't rename this model name", "Rename Model", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            rename = new Rename(this,1);
            rename.Show();
        }


        private void deleteModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to delete this model?", "Delete Model", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var data = MasterLB.GetMasterLB(id_lb);
                if (data.Count > 0)
                {
                    data[0].Delete();
                }
                loadTable();
                MessageBox.Show("Delete model success", "Delete Model", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void deleteSWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to delete this software?", "Delete Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var model = MasterLB.GetMasterLBBySW(id_sw);
                foreach(var item in model)
                {
                    item.Delete();
                }
                var data = MasterSW.GetMasterSW(id_sw);
                if (data.Count > 0)
                {
                    data[0].Delete();
                    loadTable();
                }
                 loadTable();
                 MessageBox.Show("Delete software success", "Delete Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnEdit.PerformClick();
        }
    }
}
