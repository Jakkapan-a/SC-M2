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
        Modules.ImageList Images = new Modules.ImageList();
        
        private void Setteing_Load(object sender, EventArgs e)
        {
            loadTable();
            renderPicture();
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
                    renderPicture();
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
                Edit edit = new Edit(this.model.id,this);
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

        private void renderPicture()
        {
            flowLayoutPanelSetting.Controls.Clear();
            SC_M2.Modules.ImageList image = new SC_M2.Modules.ImageList();
            image.model_id = model.id;
            var list = image.GetModel();
            foreach (var item in list)
            {
                var pb = new PictureBox();
                pb.Height = 112;
                pb.Width = 200;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = Image.FromFile(item.path);
                pb.Tag = item.id;
                pb.BorderStyle = BorderStyle.FixedSingle;
                //pb.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBox_Click);

                // Add Flow
                flowLayoutPanelSetting.Controls.Add(pb);
            }
            flowLayoutPanelSetting.Update();
        }


        //private void pictureBox_Click(object sender, MouseEventArgs e)
        //{
        //    // Picture Box
        //    PictureBox pb = (PictureBox)sender;
        //    // Get and set ID
        //    Images.id = (int)pb.Tag;
        //    toolStripStatusLabel_ImageID.Text = "Image ID: " + Images.id;

        //}
        
    }
}
