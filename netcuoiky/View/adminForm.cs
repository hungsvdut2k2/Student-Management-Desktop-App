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
        public int page = 1;

        public adminForm()
        {
            InitializeComponent();
            instance = this;
            SetFacultyCombobox();
            SetEducationalProgramCombobox();
            SetCourseCombobox();
        }

        private void SetFacultyCombobox()
        {
            facultyCombobox.Items.AddRange(Faculty_BLL.Instance.GetComboBoxItems().ToArray());
        }

        private void SetEducationalProgramCombobox()
        {
            educationalProgramCombobox.Items.AddRange(EducationalProgram_BLL.Instance.GetAllEducationalProgram().ToArray());
        }

        private void SetCourseCombobox()
        {
            courseCombobox.Items.AddRange(Course_BLL.Instance.GetAllCourse().ToArray());
        }
        
        private void facultyCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (facultyCombobox.SelectedItem != null)
            {
                string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyCombobox.SelectedIndex + 1].facultyId;
                classroomCombobox.DataSource = Classroom_BLL.Instance.GetClassByFaculty(facultyId).ToArray();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyCombobox.SelectedIndex + 1].facultyId;
            if (classroomCombobox.SelectedItem != null)
            {
                int seletectedIndex = classroomCombobox.SelectedIndex;
                string classroomId = Convert.ToString(Classroom_BLL.Instance.GetClassByFaculty(facultyId)[seletectedIndex].Value);
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

        private void addButton_Click(object sender, EventArgs e)
        {
            page = 0;
            this.Hide();
            new addFormOfAdmin().ShowDialog();
        }

        private void searchButton2_Click(object sender, EventArgs e)
        {
            string educationProgramId =
                EducationalProgram_BLL.Instance.GetAllEducationalProgram()[educationalProgramCombobox.SelectedIndex]
                    .Value.ToString();
            educationDataGridView.DataSource =
                EducationalProgram_BLL.Instance.GetAllCourseInEducationalProgram(educationProgramId);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new loginForm()).Show();
        }

        private void searchButton3_Click(object sender, EventArgs e)
        {
            string courseId = Course_BLL.Instance.GetAllCourse()[courseCombobox.SelectedIndex].Value.ToString();
            courseClassDataGridView.DataSource = CourseClassroom_BLL.Instance.GetAllClassroomOfCourse(courseId);
        }

        private void addButton2_Click(object sender, EventArgs e)
        {
            page = 1;
            this.Hide();
            new addFormOfAdmin().ShowDialog();
        }

        private void addButton3_Click(object sender, EventArgs e)
        {
            page = 2;
            this.Hide();
            new addFormOfAdmin().ShowDialog();
        }
    }
}
