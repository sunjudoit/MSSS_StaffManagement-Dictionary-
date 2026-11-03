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
    public partial class GeneralForm : Form
    {
        public static Dictionary<int, string> MasterFile = new Dictionary<int, string>();

        private void LoadStaff()
        {
            try
            {

                string filePath = "MalinStaffNamesV3.csv";
                MasterFile.Clear();

                using (StreamReader reader = new StreamReader(filePath))
                {

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (int.TryParse(data[0], out int id))
                        {
                            string Name = data[1];
                            if (!MasterFile.ContainsKey(id))
                            {
                                MasterFile.Add(id, Name);
                            }
                        }

                    }
                }

                // staus massage Staff data loaded successfully

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void DisplayStaff()
        {
            lstAllStaff.Items.Clear();

            foreach (KeyValuePair<int, string> staff in MasterFile)
            {
                lstAllStaff.Items.Add($"{staff.Key} : {staff.Value}");
            }

        }

        public GeneralForm()
        {
            InitializeComponent();
            LoadStaff();
            DisplayStaff();

        }
    
        private void txtSearchID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
             
                MessageBox.Show("Please enter numbers only.");
                return;
            }
        }


        private void SearchStaff()
        {
            lstSearchResults.Items.Clear();
            txtInfoID.Clear();
            txtInfoName.Clear();

            string searchID = txtSearchID.Text.Trim();
            string searchName = txtSearchName.Text.Replace(" ", "").ToLower();

            if (searchID == "" && searchName == "") return;

            foreach (KeyValuePair<int, string> staff in MasterFile)
            { 
                string staffID = staff.Key.ToString();
                string staffName = staff.Value.ToLower().Replace(" ", "");
                if (staffID.Contains(searchID) && staffName.Contains(searchName))
                {
                    lstSearchResults.Items.Add($"{staff.Key} : {staff.Value}");
                }
            }

        }

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
           
            SearchStaff();
        }


        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            SearchStaff();
        }

        private void GeneralForm_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.F1)
            {
                txtSearchID.Clear();
                txtSearchID.Focus();
                

            }
            else if (e.KeyCode == Keys.F2)
            {
                txtSearchName.Clear();
                txtSearchName.Focus();
            }
            else if (e.Alt && e.KeyCode == Keys.A)
            {
                
                AdminForm admin = new AdminForm(txtInfoID.Text,txtInfoName.Text);
                admin.ShowDialog(this);

            }


        }

        private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchResults.SelectedItem == null)
            {
                return;
            }

            string selectedStaff = lstSearchResults.SelectedItem.ToString();
            string[] data = selectedStaff.Split(':');
            
            txtInfoID.Text = data[0].Trim();
            txtInfoName.Text = data[1].Trim();


        }
    }
}
