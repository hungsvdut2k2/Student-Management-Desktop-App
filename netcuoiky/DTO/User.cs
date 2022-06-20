using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class User
    {
        [Key]
        [System.ComponentModel.DisplayName("MSSV")]
        public string userId { get; set; }
        [System.ComponentModel.DisplayName("Họ Và Tên")]
        public string name { get; set; }
        [System.ComponentModel.DisplayName("Dân Tộc")]
        public string nation { get; set; }
        [System.ComponentModel.DisplayName("Giới Tính")]
        public bool gender { get; set; }
        [System.ComponentModel.DisplayName("Ngày Sinh")]
        public string dob { get; set; }
        public string birthPlace { get; set; }
        public string personalId { get; set; }
        public string medicalCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public Classroom classroom { get; set; }
        public string classId { get; set; }
    }
}
