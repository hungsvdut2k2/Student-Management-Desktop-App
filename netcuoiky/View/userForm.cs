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
    public partial class userForm : Form
    {
        public userForm()
        {
            InitializeComponent();
            SetComboBox();
            SetInformation();
            SetDataSourceForClassroom();
            SetDataSourceForScore();
            SetDataSourceForEducationalProgram();
            SetDataSourceForCourse();
            LoadUserImage();
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new loginForm()).Show();
        }
        private void SetComboBox()
        {
            genderComboBox.Items.Add("Nam");
            genderComboBox.Items.Add("Nữ");
            nationComboBox.Items.Add("Kinh");
        }
        private void SetInformation()
        {
            string userId = loginForm.instance.userId;
            User user = User_BLL.Instance.GetUser(userId);
            nameTextBox.Text = user.name;
            dobTextBox.Text = user.dob;
            phoneNumberTextBox.Text = user.phoneNumber;
            birthPlaceTextBox.Text = user.birthPlace;
            emailTextBox.Text = user.email;
            if (user.gender)
            {
                genderComboBox.SelectedIndex = 0;
            }
            else
            {
                genderComboBox.SelectedIndex = 1;
            }
            nationComboBox.SelectedIndex = 0;
            personalTextBox.Text = user.personalId;
            userIdTextBox.Text = user.userId;
            medicalCodeTextBox.Text = user.medicalCode;
            Classroom classroom = Classroom_BLL.Instance.GetClassroomById(user.classId);
            Faculty faculty = Faculty_BLL.Instance.GetFacultyById(classroom.facultyId);
            classTextBox.Text = classroom.name;
            facultyTextBox.Text = faculty.name;
        }

        private void SetDataSourceForClassroom()
        {
            string userId = loginForm.instance.userId;
            User user = User_BLL.Instance.GetUser(userId);
            classroomDataGridView.DataSource = User_BLL.Instance.GetAllUserInClass(user.classId);
            for (int i = 5; i < classroomDataGridView.ColumnCount; i++)
            {
                classroomDataGridView.Columns[i].Visible = false;
            }
        }

        private void SetDataSourceForScore()
        {
            string userId = loginForm.instance.userId;
            scoreDataGridView.DataSource = Score_BLL.Instance.GetAllScoreOfStudent(userId);
            for (int i = 2; i < scoreDataGridView.ColumnCount - 2; i++)
            {
                scoreDataGridView.Columns[i].Width = 60;
            }
            scoreDataGridView.Columns[scoreDataGridView.ColumnCount - 1].Width = 60;
        }

        private void SetDataSourceForEducationalProgram()
        {
            string userId = loginForm.instance.userId;
            User user = User_BLL.Instance.GetUser(userId);
            Classroom classroom = Classroom_BLL.Instance.GetClassroomById(user.classId); 
            educationalProgramDataGridView.DataSource = EducationalProgram_BLL.Instance.GetAllCourseInEducationalProgram(classroom.educationalProgramId);
        }

        private void SetDataSourceForCourse()
        {
            string userId = loginForm.instance.userId;
            courseComboBox.DataSource = Course_BLL.Instance.FilterCourseForStudent(userId);
        }

        private void LoadUserImage()
        {
            string userId = loginForm.instance.userId;
            string folder = userId.Substring(0, 3);
            Image image1 = Image.FromFile($"E:\\DUT_Image\\{folder}\\{userId}.jpg");
            userPictureBox.Image = image1;
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string userId = loginForm.instance.userId;
            User tempUser = new User
            {
                name = nameTextBox.Text,
                birthPlace = birthPlaceTextBox.Text,
                medicalCode = medicalCodeTextBox.Text,
                dob = dobTextBox.Text,
                phoneNumber = phoneNumberTextBox.Text,
                personalId = personalTextBox.Text,
                nation = nationComboBox.Text,
                email = emailTextBox.Text
            };
            User_BLL.Instance.UpdateUser(userId, tempUser);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string userId = loginForm.instance.userId;
            string courseId = Course_BLL.Instance.FilterCourseForStudent(userId)[courseComboBox.SelectedIndex].Value.ToString();
            courseClassDataGridView.DataSource = CourseClassroom_BLL.Instance.GetAllClassroomOfCourse(courseId);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = courseClassDataGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = courseClassDataGridView.Rows[selectedIndex];
            string courseClassId = selectedRow.Cells[0].Value.ToString();
            string userId = loginForm.instance.userId;
            if (CourseClassroom_BLL.Instance.CheckConflictCourseClassroom(userId, courseClassId) == false)
            {
                MessageBox.Show("Bạn đã đăng ký lớp học phần thuộc học phần này !!!!");
            }
            else
            {
                if (Schedule_BLL.Instance.CheckConflictSchedule(userId, courseClassId) == false)
                {
                    MessageBox.Show(
                        "Bạn đã đăng ký lớp học phần trong thời gian này, Vui Lòng chọn lớp học phần khác !!!!");
                }
                else
                {
                    var newUserCourseClassroom = new UserCourseClassroom
                    {
                        UserId = userId,
                        CourseClassroomId = courseClassId,
                        User = null,
                        courseClassroom = null,
                    };
                    var newScore = new Score
                    {
                        courseClassroom = null,
                        courseClassroomId = courseClassId,
                        user = null,
                        userId = userId,
                        excerciseRate = 0.2,
                        midTermRate = 0.2,
                        finalTermRate = 0.6
                    };
                    CourseClassroom_BLL.Instance.AddStudentToCourseClassroom(newUserCourseClassroom);
                    Score_BLL.Instance.AddScore(newScore);
                    SetDataSourceForScore();
                }
            }
            
        }
    }
}
