using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class ReturnedCourseClassroom
    {
       [System.ComponentModel.DisplayName("Lớp Học Phần")]
        public string CourseClassId { get; set; }
        [System.ComponentModel.DisplayName("Giảng Viên")]
        public string TeacherName { get; set; }
        [System.ComponentModel.DisplayName("Thời Khoá Biểu")]
        public string Schedule { get; set; }
    }
}
