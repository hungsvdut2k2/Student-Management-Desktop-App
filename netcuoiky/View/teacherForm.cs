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
            SetDataSourceSchedule();
        }
        public void SetInformation()
        {
            string userId = loginForm.instance.userId;
        }
        public void SetDataSourceSchedule()
        {
            string userId = loginForm.instance.userId;
            ScheduleDataGridView.DataSource = Schedule_BLL.Instance.GetAllCourseInTheWeek(userId);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new loginForm()).Show();
        }
    }
}
