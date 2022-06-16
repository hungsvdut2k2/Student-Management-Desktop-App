using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class CourseEducationalProgram
    {
        public Course Course { get; set; }
        public string CourseId { get; set; }
        public EducationalProgram EducationalProgram { get; set; }
        public  string EducationalProgramId { get; set; }
    }
}
