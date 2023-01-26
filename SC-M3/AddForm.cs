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
    public partial class AddForm : Form
    {
        public delegate void SaveEvent();
        public event SaveEvent onSaveEvent;
        public AddForm()
        {
            InitializeComponent();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtSW;
            txtSW.Focus();

        }
        private void btSave_Click(object sender, EventArgs e)
        {
            Master_sw master_ = new Master_sw();
            master_.serial_full = txtSN.Text.Trim().Replace(" ", "");
            master_.serial_no = getSerialNo(txtSN.Text.Trim());
            master_.sw_ver = txtSW.Text.Trim().Replace(" ", "");

            if (master_.serial_full.Length == 0 || master_.sw_ver.Length == 0)
            {
                MessageBox.Show("Please fill in all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            master_.Save();
            MessageBox.Show("Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onSaveEvent.Invoke();
            this.Close();
        }

        private string getSerialNo(string txt)
        {
            txt = txt.Trim().Replace(" ", "");
            txt = txt.Remove(txt.Length - 10);
            return txt;
        }

    }
}
