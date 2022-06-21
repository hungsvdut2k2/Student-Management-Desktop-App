using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class ReturnedCourse
    {
        [System.ComponentModel.DisplayName("Mã Học Phần")]
        public string courseId { get; set; }
        [System.ComponentModel.DisplayName("Học Phần")]
        public string CourseName { get; set; }
        [System.ComponentModel.DisplayName("Tín Chỉ")]
        public int Credits { get; set; }
        [System.ComponentModel.DisplayName("Học Kì")]
        public int Semester { get; set; }
        [System.ComponentModel.DisplayName("Học Phần Học Trước")]
        public string requiremnetId { get; set; }
        [System.ComponentModel.DisplayName("Có thể đăng ký")]
        public bool isAvailable { get; set; }
    }
}
