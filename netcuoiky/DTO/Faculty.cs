using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class Faculty
    {
        [Key]
        public string facultyId { get; set; }
        public string name { get; set; }
        public List<Classroom> classrooms { get; set; }
    }
}
