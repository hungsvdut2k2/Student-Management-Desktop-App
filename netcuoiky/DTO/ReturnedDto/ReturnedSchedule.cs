using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO.ReturnedDto
{
    public class ReturnedSchedule
    {
        [System.ComponentModel.DisplayName("Môn học")]
        public string CourseName { get; set; }
        [System.ComponentModel.DisplayName("Ngày dạy")]
        public string Day { get; set; }
        [System.ComponentModel.DisplayName("Khung giờ")]
        public string Time { get; set; }
    }
}
