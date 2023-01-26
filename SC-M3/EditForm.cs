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
    public partial class EditForm : Form
    {
        private int id = -1;
        public EditForm(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        public delegate void UpdateEvent();
        public UpdateEvent onUpdateEvent;

        Master_sw master_;
        private void EditForm_Load(object sender, EventArgs e)
        {
            master_ = Master_sw.LoadById(id);
            txtSN.Text = master_.serial_full;
            txtSW.Text = master_.sw_ver;
        }

        private string getSerialNo(string txt)
        {
            txt = txt.Trim().Replace(" ", "");
            txt = txt.Remove(txt.Length - 10);
            return txt;
        }
        private void btUpdate_Click(object sender, EventArgs e)
        {
            master_.serial_full = txtSN.Text.Trim().Replace(" ", "");
            master_.serial_no = getSerialNo(txtSN.Text.Trim());
            master_.sw_ver = txtSW.Text.Trim().Replace(" ", "");
            master_.Update();

            MessageBox.Show("Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onUpdateEvent?.Invoke();
            this.Close();
        }
    }
}
