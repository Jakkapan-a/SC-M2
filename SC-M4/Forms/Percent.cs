using SC_M4.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC_M4.Forms
{
    public partial class Percent : Form
    {
        private int id = -1;
        public Percent(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        List<Setting> settings= new List<Setting>();
        private void Percent_Load(object sender, EventArgs e)
        {
            settings = Setting.GetSettingID(id);
            numericUpDown1.Value = settings[0].percent;
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            if(id != -1)
            {
                settings[0].percent = (int)numericUpDown1.Value;
                settings[0].Update();
                this.Close();
            }
        }

    }
}
