using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class CourseClassroom
    {
        [Key]
        public string CourseClassId { get; set; }
        public string TeacherName { get; set; }
        public Course course { get; set; }
        public string courseId { get; set; }
    }
}
