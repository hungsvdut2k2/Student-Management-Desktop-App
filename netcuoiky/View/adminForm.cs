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




        private void DeleteButton3_Click_1(object sender, EventArgs e)
        {
            string courseId = Course_BLL.Instance.GetAllCourse()[courseCombobox.SelectedIndex].Value.ToString();
            string courseClassId =
                CourseClassroom_BLL.Instance.GetAllClassroomOfCourse(courseId)[
                    courseClassDataGridView.SelectedCells[0].RowIndex].CourseClassId;
            CourseClassroom_BLL.Instance.DeleteCourseClass(courseClassId);
            courseClassDataGridView.DataSource = CourseClassroom_BLL.Instance.GetAllClassroomOfCourse(courseId);
        }

        private void deleteButton2_Click_1(object sender, EventArgs e)
        {
            string educationProgramId =
                EducationalProgram_BLL.Instance.GetAllEducationalProgram()[educationalProgramCombobox.SelectedIndex]
                    .Value.ToString();
            string courseId =
                EducationalProgram_BLL.Instance.GetAllCourseInEducationalProgram(educationProgramId)[
                    educationDataGridView.SelectedCells[0].RowIndex].courseId;
            EducationalProgram_BLL.Instance.DeleteCourseInEducationalProgram(courseId, educationProgramId);
            educationDataGridView.DataSource =
                EducationalProgram_BLL.Instance.GetAllCourseInEducationalProgram(educationProgramId);
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            string userId = userDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            Account_BLL.Instance.DeleteAccount(userId);
            Score_BLL.Instance.DeleteScore(userId);
            User_BLL.Instance.DeleteUser(userId);
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

        private void updateButton_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = userDataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = userDataGridView.Rows[selectedRowIndex];
            string userId = Convert.ToString(selectedRow.Cells[0].Value);
            string Name = Convert.ToString(selectedRow.Cells[1].Value);
            string Nation = Convert.ToString(selectedRow.Cells[2].Value);
            bool Gender = Convert.ToBoolean(selectedRow.Cells[3].Value);
            string Dob = Convert.ToString(selectedRow.Cells[4].Value);
            User tempUser = new User
            {
                name = Name,
                nation = Nation,
                gender = Gender,
                dob = Dob
            };
            User_BLL.Instance.UpdateUser(userId, tempUser);
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

        private void updateButton2_Click(object sender, EventArgs e)
        {
            string educationProgramId =
                EducationalProgram_BLL.Instance.GetAllEducationalProgram()[educationalProgramCombobox.SelectedIndex]
                    .Value.ToString();
            int selectedRowIndex = educationDataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = educationDataGridView.Rows[selectedRowIndex];
            string courseId = selectedRow.Cells[0].Value.ToString();
            string courseName = selectedRow.Cells[1].Value.ToString();
            int credits = Convert.ToInt32(selectedRow.Cells[2].Value);
            int semester = Convert.ToInt32(selectedRow.Cells[3].Value);
            string requirementId = Convert.ToString(selectedRow.Cells[4].Value);
            CourseEducationalProgram temProgram = new CourseEducationalProgram
            {
                CourseId = courseId,
                Semester = semester,
                Course = null,
                EducationalProgramId = educationProgramId,
                EducationalProgram = null
            };
            if (Course_BLL.Instance.checkCourseExist(courseId))
            {
                Course course = new Course
                {
                    courseId = courseId,
                    Credits = credits,
                    IsAvailable = true,
                    name = courseName,
                    requirementId = requirementId
                };
                Course_BLL.Instance.UpdateCourse(courseId, course);
                EducationalProgram_BLL.Instance.UpdateEducationalProgram(courseId, educationProgramId, temProgram);
            }
            else
            {
                Course newCourse = new Course
                {
                    courseId = courseId,
                    name = courseName,
                    Credits = credits,
                    IsAvailable = true,
                    requirementId = requirementId
                };
                Course_BLL.Instance.AddCourse(newCourse);
                EducationalProgram_BLL.Instance.UpdateEducationalProgram(courseId, educationProgramId, temProgram);
            }
            educationDataGridView.DataSource =
                EducationalProgram_BLL.Instance.GetAllCourseInEducationalProgram(educationProgramId);
        }
    }
}
