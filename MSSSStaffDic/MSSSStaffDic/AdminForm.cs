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
        private string currentMode = "";
        private bool hasSelectedStaff = false;
        private string originalStaffID = "";

        public AdminForm(string staffID, string staffName)
        {
            InitializeComponent();

            txtAdminID.MaxLength = 9;

            txtAdminID.Text = staffID;
            txtAdminName.Text = staffName;

            txtAdminID.ReadOnly = true;
            txtAdminName.ReadOnly = true;

            hasSelectedStaff = int.TryParse(staffID, out int id) &&
                       GeneralForm.MasterFile.ContainsKey(id);

            originalStaffID = staffID;
        }

        private void CreateInputStaff()
        {
            if (currentMode != "")
            {
                MessageBox.Show("Please complete the current operation first.");
                return;
            }

            currentMode = "Create";

            txtAdminID.ReadOnly = false;
            txtAdminName.ReadOnly = false;

            txtAdminID.Text = "77";
            txtAdminName.Clear();

            txtAdminID.Focus();
            txtAdminID.SelectionStart = txtAdminID.Text.Length;
           
        }
        private bool CreateStaff()
        {
            // Validate Staff ID
            if (!int.TryParse(txtAdminID.Text, out int staffID) ||
                txtAdminID.Text.Length != 9 ||
                !txtAdminID.Text.StartsWith("77"))
            {
                MessageBox.Show("Staff ID must start with 77 and be 9 digits.");
                return false;
            }

            // Validate Staff Name
            if (string.IsNullOrWhiteSpace(txtAdminName.Text))
            {
                MessageBox.Show("Please enter a staff name.");
                return false;
            }

            // Check duplicate Staff ID
            if (GeneralForm.MasterFile.ContainsKey(staffID))
            {
                MessageBox.Show("Staff ID already exists.");
                return false;
            }

            // Add staff to Dictionary
            GeneralForm.MasterFile.Add(staffID, txtAdminName.Text.Trim());

            return true;

        }
        private void UpdateInputStaff()
        {
            if (currentMode != "")
            {
                MessageBox.Show("Please complete the current operation first.");
                return;
            }

            currentMode = "Update";

            txtAdminName.ReadOnly = false;
            txtAdminName.Focus();

        }
        private bool UpdateStaff()
        {
            int staffID = int.Parse(txtAdminID.Text);

            // Validate Staff Name
            if (string.IsNullOrWhiteSpace(txtAdminName.Text))
            {
                MessageBox.Show("Please enter a staff name.");
                return false;
            }

            // Update staff name in Dictionary
            GeneralForm.MasterFile[staffID] = txtAdminName.Text.Trim();

            return true;
        }
       
        private void DeleteInputStaff()
        {
            if (currentMode != "")
            {
                MessageBox.Show("Please complete the current operation first.");
                return;
            }

            currentMode = "Delete";

            txtAdminID.Clear();
            txtAdminName.Clear();

        }
        private bool DeleteStaff()
        {
            int staffID = int.Parse(originalStaffID);

            if (!GeneralForm.MasterFile.Remove(staffID))
            {
                MessageBox.Show("Failed to delete staff.");
                return false;
            }

            return true;
        }


        private void AdminForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.L)
            {
                if (currentMode == "Create")
                {
                    if (!CreateStaff())
                    {
                        return;
                    }
                }
                else if (currentMode == "Update")
                {
                    if (!UpdateStaff())
                    {
                        return;
                    }
                }
                else if (currentMode == "Delete")
                {
                    if (!DeleteStaff())
                    {
                        return;
                    }
                }

                this.Close();
            }
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                CreateInputStaff();
            }
            else if (e.Alt && e.KeyCode == Keys.U)
            {
                if (hasSelectedStaff)
                {
                    UpdateInputStaff();
                }
            }
            else if (e.Alt && e.KeyCode == Keys.D)
            {
                if (hasSelectedStaff)
                {
                    DeleteInputStaff();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
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
