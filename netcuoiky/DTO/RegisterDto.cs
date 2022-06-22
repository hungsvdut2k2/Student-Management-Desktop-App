using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
     public class RegisterDto
    {
        public string className { get; set; }
        public string userId { get; set; }
        public string studentName { get; set; }
        public bool Gender { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
        public string Role { get; set; }
    }
}
