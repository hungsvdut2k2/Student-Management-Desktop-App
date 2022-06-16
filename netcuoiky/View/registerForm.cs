using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using netcuoiky.BLL;
using netcuoiky.DTO;

namespace netcuoiky
{
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
        }

        public void SetComboBox()
        {
            roleCombobox.Items.Add("Admin");
            roleCombobox.Items.Add("Teacher");
            roleCombobox.Items.Add("Student");
            facultyComboBox.Items.AddRange(Faculty_BLL.Instance.GetComboBoxItems().ToArray());
        }

        private void registerForm_Load(object sender, EventArgs e)
        {
            SetComboBox();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string username = userNameTextBox.Text;
            string password = passwordTextBox.Text;
            string role = roleCombobox.SelectedItem.ToString();
            string userId = userNameTextBox.Text;
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyComboBox.SelectedIndex + 1].facultyId;
            int seletectedIndex = classroomComboBox.SelectedIndex;
            string classroomId = Convert.ToString(Classroom_BLL.Instance.GetClassByFaculty(facultyId)[seletectedIndex].Value);
            Account_BLL.Instance.Register(username, password, role, userId, classroomId);
            MessageBox.Show("Dang Ky Thanh Cong");
            this.Dispose();
            new adminForm().ShowDialog();
        }

        private void facultyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyComboBox.SelectedIndex + 1].facultyId;
            classroomComboBox.Items.AddRange(Classroom_BLL.Instance.GetClassByFaculty(facultyId).ToArray());
        }
    }
}
