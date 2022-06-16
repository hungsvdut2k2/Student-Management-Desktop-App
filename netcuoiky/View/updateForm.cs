using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netcuoiky
{
    public partial class updateForm : Form
    {
        public updateForm()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            nameTextBox.Enabled = true;
            dobTextBox.Enabled = true;
            birthPlaceTextBox.Enabled = true;
            personalIdTextBox.Enabled = true;
            medicalCodeTextBox.Enabled = true;
            userIdTextBox.Enabled = true;
            phoneNumberTextBox.Enabled = true;
            genderComboBox.Enabled = true;
            nationTextBox.Enabled = true;
        }

        private void updateForm_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
