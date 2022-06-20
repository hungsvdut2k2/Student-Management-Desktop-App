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
    public partial class addFormOfAdmin : Form
    {
        public addFormOfAdmin()
        {
            InitializeComponent();
            SetTab1ComboBox();
            SetTab2ComboBox();
            SetTab3ComboBox();
        }
        public void SetTab1ComboBox()
        {
            roleCombobox.Items.Add("Admin");
            roleCombobox.Items.Add("Teacher");
            roleCombobox.Items.Add("Student");
            facultyComboBox2.Items.AddRange(Faculty_BLL.Instance.GetComboBoxItems().ToArray());
        }
        private void SetTab3ComboBox()
        {
            CourseComboBox.Items.AddRange(Course_BLL.Instance.GetAllCourse().ToArray());
            facultyComboBox.Items.AddRange(Faculty_BLL.Instance.GetComboBoxItems().ToArray());
            dateComboBox.Items.Add("Thứ Hai");
            dateComboBox.Items.Add("Thứ Ba");
            dateComboBox.Items.Add("Thứ Tư");
            dateComboBox.Items.Add("Thứ Năm");
            dateComboBox.Items.Add("Thứ Sáu");
            dateComboBox.Items.Add("Thứ Bảy");
            dateComboBox.Items.Add("Chủ Nhật");
            for (int i = 1; i <= 10; i++)
            {
                startPeriodComboBox.Items.Add(i);
            }
        }

        private void SetTab2ComboBox()
        {
            availableTextBox.Items.Add("Đã có");
            availableTextBox.Items.Add("Chưa có");

        }

        private void addFormOfAdmin_Load(object sender, EventArgs e)
        {
            this.guna2TabControl1.SelectedIndex = adminForm.instance.page;
        }

        private void facultyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyComboBox.SelectedIndex + 1].facultyId;
            string teacherClassId = facultyId + "2000";
            teacherComboBox.DataSource = User_BLL.Instance.GetAllUserInCLassComboboxItems(teacherClassId);
        }

        private void startPeriodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> availablePeriods = new List<int>();
            for (int i = startPeriodComboBox.SelectedIndex + 2; i <= 10; i++)
            {
                availablePeriods.Add(i);
            }

            endPeriodComboBox.DataSource = availablePeriods;
        }

        private void addButton3_Click(object sender, EventArgs e)
        {
            string courseId = Course_BLL.Instance.GetAllCourse()[CourseComboBox.SelectedIndex].Value.ToString();
            string courseClassId = courseClassTextBox.Text;
            string teacherName = teacherComboBox.Text;
            int startPeriod = Convert.ToInt32(startPeriodComboBox.Text);
            int endPeriod = Convert.ToInt32(endPeriodComboBox.Text);
            int dateInWeek = Schedule.ConvertDateToNumber(dateComboBox.Text);
            Course course = Course_BLL.Instance.GetCourseById(courseId);
            var newCourseClass = new CourseClassroom
            {
                CourseClassId = courseClassId,
                course = null,
                courseId = course.courseId,
                TeacherName = teacherName,
                Schedules = null,
                IsComplete = false
            };
            var newSchedule = new Schedule
            {
                CourseClassroom = null,
                CourseClassId = courseClassId,
                StartPeriod = startPeriod,
                EndPeriod = endPeriod,
                DateInWeek = dateInWeek,
            };
            CourseClassroom_BLL.Instance.AddCourseClass(newCourseClass);
            Schedule_BLL.Instance.AddSchedule(newSchedule);
        }


        private void facultyComboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyComboBox2.SelectedIndex + 1].facultyId;
            classroomComboBox.Items.AddRange(Classroom_BLL.Instance.GetClassByFaculty(facultyId).ToArray());
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string username = userNameTextBox.Text;
            string password = passwordTextBox.Text;
            string role = roleCombobox.SelectedItem.ToString();
            string userId = userNameTextBox.Text;
            string facultyId = Faculty_BLL.Instance.GetAllFaculties()[facultyComboBox2.SelectedIndex + 1].facultyId;
            int seletectedIndex = classroomComboBox.SelectedIndex;
            string classroomId = Convert.ToString(Classroom_BLL.Instance.GetClassByFaculty(facultyId)[seletectedIndex].Value);
            Account_BLL.Instance.Register(username, password, role, userId, classroomId);
            MessageBox.Show("Dang Ky Thanh Cong");
            this.Dispose();
            new adminForm().ShowDialog();
        }

        private void addButton2_Click_1(object sender, EventArgs e)
        {
            if (availableTextBox.SelectedIndex == 0)
            {
                var courseEducationalProgram = new CourseEducationalProgram
                {
                    CourseId = Course_BLL.Instance.GetAllCourse()[courseComboBox2.SelectedIndex].Value.ToString(),
                    Course = null,
                    EducationalProgramId =
                        EducationalProgram_BLL.Instance.GetAllEducationalProgram()[
                            educationalProgramCombobox.SelectedIndex].Value.ToString(),
                    EducationalProgram = null,
                    Semester = Convert.ToInt32(semesterTextBox.Text)
                };
                EducationalProgram_BLL.Instance.AddCourseToEducationalProgram(courseEducationalProgram);
            }
            else
            {
                string courseId = courseIdTextBox.Text;
                string courseName = courseNameTextBox.Text;
                int credits = Convert.ToInt32(creditTextBox.Text);
                var newCourse = new Course
                {
                    courseId = courseId,
                    name = courseName,
                    Credits = credits,
                    IsAvailable = true
                };
                Course_BLL.Instance.AddCourse(newCourse);
            }
        }

        private void availableTextBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (availableTextBox.SelectedIndex == 0)
            {
                courseIdTextBox.Enabled = false;
                courseNameTextBox.Enabled = false;
                creditTextBox.Enabled = false;
                courseComboBox2.Enabled = true;
                educationalProgramCombobox.Enabled = true;
                semesterTextBox.Enabled = true;
                courseComboBox2.DataSource = Course_BLL.Instance.GetAllCourse();
                educationalProgramCombobox.DataSource = EducationalProgram_BLL.Instance.GetAllEducationalProgram();
            }
            else
            {
                courseIdTextBox.Enabled = true;
                courseNameTextBox.Enabled = true;
                creditTextBox.Enabled = true;
                courseComboBox2.Enabled = false;
                educationalProgramCombobox.Enabled = false;
                semesterTextBox.Enabled = false;
            }
        }
    }
}
