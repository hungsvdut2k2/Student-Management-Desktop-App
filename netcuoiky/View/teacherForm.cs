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

namespace netcuoiky.View
{
    public partial class teacherForm : Form
    {
        public static teacherForm instance;
        public teacherForm()
        {
            InitializeComponent();
            instance = this;
            SetInformation();
            SetDataSourceSchedule();
            SetCourseClassComboBox();
        }
        private void SetInformation()
        {
            string userId = loginForm.instance.userId;
            User user = User_BLL.Instance.GetUser(userId);
            Classroom classroom = Classroom_BLL.Instance.GetClassroomById(user.classId);
            Faculty faculty = Faculty_BLL.Instance.GetFacultyById(classroom.facultyId);
            nameTextBox.Text = user.name;
            nationTextbox.Text = user.nation;
            //genderComboBox.Items.Add("Nam");
            //genderComboBox.Items.Add("Nữ");
            if (user.gender)
            {
                genderComboBox.Items.Add("Nam");
            }
            else
            {
                genderComboBox.Items.Add("Nữ");
            }

            dobTextBox.Text = user.dob;
            birthPlaceTextBox.Text = user.birthPlace;
            personalIdTextBox.Text = user.personalId;
            medicalCodeTextBox.Text = user.medicalCode;
            emailTextBox.Text = user.email;
            userIdTextBox.Text = user.userId;
            phoneNumberTextBox.Text = user.phoneNumber;
            classroomIdTextBox.Text = classroom.name;
            facultyTextBox.Text = faculty.name;
        }
        private void SetDataSourceSchedule()
        {
            string userId = loginForm.instance.userId;
            User user = User_BLL.Instance.GetUser(userId);
            ScheduleDataGridView.DataSource = CourseClassroom_BLL.Instance.GetAllTeacherCourseClassrooms(user.name);
        }

        private void SetCourseClassComboBox()
        {
            string userId = loginForm.instance.userId;
            User user = User_BLL.Instance.GetUser(userId);
            courseClassComboBox.DataSource = CourseClassroom_BLL.Instance.GetComboboxItems(user.name);
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginForm.instance.Show();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            bool temp;
            string userId = loginForm.instance.userId;
            if (genderComboBox.SelectedIndex == 0)
            {
                temp = true;
            }
            else
            {
                temp = false;
            }
            User tempUser = new User
            {
                name = nameTextBox.Text,
                gender = temp,
                birthPlace = birthPlaceTextBox.Text,
                medicalCode = medicalCodeTextBox.Text,
                dob = dobTextBox.Text,
                phoneNumber = phoneNumberTextBox.Text,
                personalId = personalIdTextBox.Text,
                nation = nationTextbox.Text,
                email = emailTextBox.Text
            };
            User_BLL.Instance.UpdateUser(userId, tempUser);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string courseClassId = courseClassComboBox.SelectedValue.ToString();
            scoreDataGridView.DataSource = Score_BLL.Instance.GetScoreOfCourseClass(courseClassId);
        }

        private void updateButton2_Click(object sender, EventArgs e)
        {
            int selectedIndex = scoreDataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow row = scoreDataGridView.Rows[selectedIndex];
            string courseClassId = courseClassComboBox.SelectedValue.ToString();
            string userId = row.Cells[0].Value.ToString();
            double excerciseScore = Convert.ToDouble(row.Cells[2].Value);
            double midTermScore = Convert.ToDouble(row.Cells[3].Value);
            double finalTermScore = Convert.ToDouble(row.Cells[4].Value);
            Score tempScore = new Score
            {
                excerciseScore = excerciseScore,
                midTermScore = midTermScore,
                finalTermScore = finalTermScore
            };
            Score_BLL.Instance.UpdateScore(userId, courseClassId, tempScore);
            scoreDataGridView.DataSource = Score_BLL.Instance.GetScoreOfCourseClass(courseClassId);
        }
    }
}
