using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class Classroom
    {
        [Key]
        public string classId { get; set; }
        public string name { get; set; }
        public Faculty faculty { get; set; }
        public string facultyId { get; set; }
        public List<User> users { get; set; }
        public EducationalProgram educationalProgram { get; set; }
        public string educationalProgramId { get; set; }
    }
}
