using SC_M2.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M2
{
    public partial class Setteing : Form
    {
        public Setteing()
        {
            InitializeComponent();
        }
        Model model = new Model();
        private void Setteing_Load(object sender, EventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {

            dataGridViewModel.DataSource = null;
            Model m = new Model();

            List<Model> ml = m.GetAll();
            int num = 1;

            var ml2 = (from x in ml
                       select new
                       {
                           ID = x.id,
                           No = num++,
                           Model = x.name,
                           Full_Name = x.fullname,
                           Accept = x.percent,
                           Date = x.created_at,
                           Update = x.updated_at
                       }).ToList();

            dataGridViewModel.DataSource = ml2;
            dataGridViewModel.Columns[0].Visible = false;
            dataGridViewModel.Columns[1].Width = (int)(dataGridViewModel.Width * 0.1);
            dataGridViewModel.Columns[dataGridViewModel.ColumnCount - 1].Visible = false;
            dataGridViewModel.Columns[dataGridViewModel.ColumnCount - 2].Width = (int)(dataGridViewModel.Width * 0.3);
            dataGridViewModel.Update();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add a = new Add(this);
            a.ShowDialog();
        }

        private void dataGridViewModel_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewModel.SelectedRows.Count > 0)
                {
                    dynamic row = dataGridViewModel.SelectedRows[0].DataBoundItem;
                    this.model.id = row.ID;
                    toolStripStatusLabelID.Text = "ID : " + model.id.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.model.id == 0)
                {
                    throw new Exception("Model is empty!");
                }
                Edit edit = new Edit(this.model.id);
                edit.ShowDialog();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
         
        }

        private void tbLoadTable_Click(object sender, EventArgs e)
        {
            loadTable();
        }
    }
}
