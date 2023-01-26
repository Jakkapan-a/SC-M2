using SC_M3.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M3
{
    public partial class Setting : Form
    {
        private int id = -1;
        public Setting()
        {
            InitializeComponent();
        }
        private AddForm _addForm;
        private void btAdd_Click(object sender, EventArgs e)
        {
            if(_addForm!=null)
            {
                _addForm.Close();
                _addForm.Dispose();
            }
            _addForm= new AddForm();
            _addForm.onSaveEvent += LoadTableDataEvent;
            _addForm.Show();
        }

        private void LoadTableDataEvent()
        {
            LoadTable();
        }

        private EditForm _editForm;
        private void btEdit_Click(object sender, EventArgs e)
        {
            if (_editForm != null)
            {
                _editForm.Close();
                _editForm.Dispose();
            }
            _editForm = new EditForm(id);
            _editForm.onUpdateEvent+= LoadTableDataEvent;
            _editForm.Show();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this item?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var data = Master_sw.LoadById(id);
                data.Delete();
                MessageBox.Show("Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTable();
            }
        }

        private void LoadTable()
        {

            dataGridView.DataSource = null;
            
            var list = Master_sw.LoadAll();
            int i = 0;
            list.Reverse();

            var data = (from m in list
                        select new
                        {
                            ID =m.id,
                            No = ++i,
                            SW_VER = m.sw_ver,
                            SN = m.serial_no,
                            SN_Full = m.serial_full,
                            Date = m.updated_at
                        }).ToList();
            dataGridView.DataSource = data;
            dataGridView.Columns[0].Visible = false;
            // 10% width
            dataGridView.Columns[1].Width = (int)(dataGridView.Width * 0.1);
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // Get id 
            if (dataGridView.SelectedRows.Count > 0)
            {
                id = int.Parse(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                toolStripStatusId.Text = "ID :"+id.ToString();
            }
        }
    }
}
