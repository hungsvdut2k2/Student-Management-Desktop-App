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

namespace netcuoiky
{
    public partial class adminForm : Form
    {
        public static adminForm instance;
        private bool isCollapsed = true;
        public adminForm()
        {
            InitializeComponent();
            instance = this;
            SetComboBox();
        }

        private void SetComboBox()
        {
            facultyCombobox.Items.AddRange(Faculty_BLL.Instance.GetComboBoxItems().ToArray());
            classroomCombobox.Items.Add("Tất Cả Lớp");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed == true)
            {
                panelDrop.Height += 10;
                if(B1.BorderRadius >= 0) { B1.BorderRadius -= 5; }
                if(panelDrop.Size == panelDrop.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                panelDrop.Height -= 10;
                if (B1.BorderRadius <= 15) { B1.BorderRadius += 5; }
                if (panelDrop.Size == panelDrop.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new loginForm()).Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new registerForm().Show();
        }


        private void facultyCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyCombobox.SelectedIndex + 1].facultyId;
            classroomCombobox.Items.AddRange(Classroom_BLL.Instance.GetClassByFaculty(facultyId).ToArray());
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyCombobox.SelectedIndex + 1].facultyId;
            if (classroomCombobox.SelectedIndex != 0)
            {
                int seletectedIndex = classroomCombobox.SelectedIndex;
                string classroomId = Convert.ToString(Classroom_BLL.Instance.GetClassByFaculty(facultyId)[seletectedIndex - 1].Value);
                userDataGridView.DataSource = User_BLL.Instance.GetAllUserInClass(classroomId);
            }
            else
            {
                userDataGridView.DataSource = User_BLL.Instance.GetAllUserInFaculty(facultyId);
            }
            for (int i = 5; i < userDataGridView.ColumnCount; i++)
            {
                userDataGridView.Columns[i].Visible = false;
            }
        }
    }
}
