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
        public string userId { get; set; }
        public string name { get; set; }
        public string nation { get; set; }
        public bool gender { get; set; }
        public string dob { get; set; }
        public string birthPlace { get; set; }
        public string personalId { get; set; }
        public string medicalCode { get; set; }
        public string phoneNumber { get; set; }
        public Classroom classroom { get; set; }
        public string classId { get; set; }
    }
}
