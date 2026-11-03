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

            foreach (KeyValuePair<int,string> staff in MasterFile)
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
    }
}
