using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class ReturnedScore
    {
        [System.ComponentModel.DisplayName("Lớp Học Phần")]
        public string courseClassId { get; set; }
        [System.ComponentModel.DisplayName("Học Phần")]
        public string courseName { get; set; }
        [System.ComponentModel.DisplayName("Tín Chỉ")]
        public int Credits { get; set; }
        [System.ComponentModel.DisplayName("Điểm Bài Tập")]
        public double excerciseScore { get; set; }
        [System.ComponentModel.DisplayName("Điểm Giữa Kì")]
        public double midTermScore { get; set; }
        [System.ComponentModel.DisplayName("Điểm Cuối Kì")]
        public double finalTermScore { get; set; }
        [System.ComponentModel.DisplayName("Tỉ Lệ")]
        public string courseScoreRate { get; set; }
    }
}
