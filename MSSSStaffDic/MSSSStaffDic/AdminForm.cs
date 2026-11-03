using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSSSStaffDic
{
    public partial class AdminForm : Form
    {
        public AdminForm(string staffID, string staffName)
        {
            InitializeComponent();

            txtAdminID.Text = staffID;
            txtAdminName.Text = staffName;

            txtAdminID.ReadOnly = true;
            txtAdminName.ReadOnly = true;

        }

        private void CreatStaffInput()
        {
            txtAdminID.ReadOnly = false;
            txtAdminName.ReadOnly = false;

            txtAdminID.Text = "77";
            txtAdminID.Focus();
            txtAdminID.SelectionStart = txtAdminID.Text.Length;


        }

        private void AdminForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.L)
            {
                this.Close();
            }
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                CreatStaffInput();
            }
            
        }

        private void txtAdminID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAdminID.ReadOnly)
            {
                return;
            }

            txtAdminID.MaxLength = 9;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only.");
                return;
            }

        }
    }
}
