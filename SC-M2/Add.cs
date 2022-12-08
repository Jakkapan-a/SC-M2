using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SC_M2.Modules;
namespace SC_M2
{
    public partial class Add : Form
    {
        Setteing setteing;
        public Add(Setteing s)
        {
            InitializeComponent();
            this.setteing = s;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbName.Text == String.Empty)
                {
                    throw new Exception("Please enter name");
                }
                Model m = new Model();
                m.fullname = tbName.Text.Trim();
                m.name = tbName.Text.Trim().Substring(0, tbName.Text.Trim().Length - 10);
                m.percent = Convert.ToInt32(tbAccept.Value);

                if (m.isName())
                {
                    throw new Exception("Name is exist");
                }
                // Save
                m.Save();
                MessageBox.Show("Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbName.Text = String.Empty;
                setteing.loadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
