using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MSSSStaffDic
{
    public partial class AdminForm : Form
    {
        public AdminForm(string staffID, string staffName)
        {
            InitializeComponent();

            txtAdminID.MaxLength = 9;

            txtAdminID.Text = staffID;
            txtAdminName.Text = staffName;

            txtAdminID.ReadOnly = true;
            txtAdminName.ReadOnly = true;

        }

        private void CreatStaff()
        {
            txtAdminID.ReadOnly = false;
            txtAdminName.ReadOnly = false;

            txtAdminID.Text = "77";
            txtAdminName.Clear();

            txtAdminID.Focus();
            txtAdminID.SelectionStart = txtAdminID.Text.Length;

            //validate staff ID
            if (!int.TryParse(txtAdminID.Text, out int staffID) || txtAdminID.Text.Length != 9 ||
                !txtAdminID.Text.StartsWith("77"))
            {
                MessageBox.Show("Staff ID must start with 77 and be 9 digits.");
                return;
            }
           


        }
        private void UpdateStaff()
        {
            txtAdminName.ReadOnly = false;
            txtAdminName.Focus();

        }
        private void DeleteStaff()
        {
            
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
                CreatStaff();
            }
            else if (e.Alt && e.KeyCode == Keys.U)
            {
                UpdateStaff();
            }
            else if (e.Alt && e.KeyCode == Keys.D)
            {
                DeleteStaff();
            }

        }

        private void txtAdminID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAdminID.ReadOnly)
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only.");
                return;
            }
            

        }
    }
}
