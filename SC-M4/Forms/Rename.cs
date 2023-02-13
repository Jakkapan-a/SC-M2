
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
    public partial class Rename : Form
    {
        Edit_Items _edit_Items;
        SettingModel _settingModel;
        int type = -1;
        public Rename(Edit_Items Edit_Items, int type)
        {
            InitializeComponent();
            _edit_Items = Edit_Items;
            this.type = type;
        }

        public Rename(SettingModel setting, int type)
        {
            InitializeComponent();
            _settingModel = setting;
            this.type = type;
        }

        int id_sw;
        int id_lb;
        private void Edit_Item_Load(object sender, EventArgs e)
        {
            try
            {
                if (_edit_Items != null)
                {
                    id_lb = _edit_Items.id_lb;
                    id_sw = _edit_Items.id_sw;
                }
                else if (_settingModel != null)
                {
                    id_lb = _settingModel.id_lb;
                    id_sw = _settingModel.id_sw;
                }
                else
                {
                    this.Close();
                }

                if (this.type == -1)
                {
                    this.Close();
                }

                if (this.type == 0)
                {
                    lbTitle.Text = "Rename Software";
                    var data = MasterSW.GetMasterSW(id_sw);
                    txtInput.Text = data[0].name;
                }
                else if (this.type == 1)
                {
                    lbTitle.Text = "Rename Model";
                    var data = MasterLB.GetMasterLB(id_lb);
                    txtInput.Text = data[0].name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.type == 0)
                {
                    var data = MasterSW.GetMasterSW(id_sw);
                    data[0].name = txtInput.Text.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");
                    if (MasterSW.IsExist(data[0].name))
                    {
                        MessageBox.Show("SW name is exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    data[0].Update();

                }
                else if (this.type == 1)
                {
                    var data = MasterLB.GetMasterLB(id_lb);
                    data[0].name = txtInput.Text.Trim().Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");
                    if (MasterLB.IsExist(data[0].name))
                    {
                        MessageBox.Show("Model name is exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    data[0].Update();
                }

                if (_edit_Items != null)
                {
                    if (this.type == 0)
                    {
                        _edit_Items?.loadTable_SW();
                    }
                    else if (this.type == 1)
                    {
                        _edit_Items?.loadTable_LB();
                    }
                }
                //else if (_settingModel != null)
                //{
                //    _settingModel.loadTable();
                //}
                _settingModel?.loadTable();
                _edit_Items?.reloadTableSetting();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
