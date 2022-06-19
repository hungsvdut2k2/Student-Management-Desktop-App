using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class Course
    {
        [Key]
        public string courseId { get; set; }
        public string name { get; set; }
        public int Credits { get; set; }
        public bool IsAvailable { get; set; }
    }
}
