using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class EducationalProgram_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static EducationalProgram_BLL _instance;

        public static EducationalProgram_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EducationalProgram_BLL();
                }

                return _instance;
            }
            private set {}
        }

        public List<Course> GetAllCourseInEducationalProgram(string educationalProgramId)
        {
            List<CourseEducationalProgram> courseEducational =
                _context.CourseEducationalProgram.Where(w => 
                    w.EducationalProgramId == educationalProgramId).ToList();
            List<ReturnedCourse> courseList = new List<ReturnedCourse>();
            List<Course> courses = new List<Course>();
            foreach (var item in courseEducational)
            {
                Course course = _context.Course.Find(item.CourseId);
                ReturnedCourse res = new ReturnedCourse
                {
                    CourseName = course.name,
                    Credits = course.Credits,
                    Semester = item.Semester
                };
                courseList.Add(res);
                courses.Add(course);
            }

            return courses;
        }
    }
}
