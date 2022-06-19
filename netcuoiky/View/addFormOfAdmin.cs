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
            SetComboBox();
        }

        private void SetComboBox()
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
    }
}
