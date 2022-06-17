using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class ReturnedCourse
    {
        [System.ComponentModel.DisplayName("Học Phần")]
        public string CourseName { get; set; }
        [System.ComponentModel.DisplayName("Tín Chỉ")]
        public int Credits { get; set; }
        [System.ComponentModel.DisplayName("Học Kì")]
        public int Semester { get; set; }
    }
}
