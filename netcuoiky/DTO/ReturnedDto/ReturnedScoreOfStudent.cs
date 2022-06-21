using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO.ReturnedDto
{
    public class ReturnedScoreOfStudent
    {
        [System.ComponentModel.DisplayName("MSSV")]
        public string studentId { get; set; }
        [System.ComponentModel.DisplayName("Tên")]
        public string studentName { get; set; }
        [System.ComponentModel.DisplayName("Điểm Bài Tập")]
        public double? excerciseScore { get; set; }
        [System.ComponentModel.DisplayName("Điểm Giữa Kì")]
        public double? midTermScore { get; set; }
        [System.ComponentModel.DisplayName("Điểm Cuối Kì")]
        public double? finalTermScore { get; set; }
        [System.ComponentModel.DisplayName("Tổng kết")]
        public double? averageScore { get; set; }
    }
}
