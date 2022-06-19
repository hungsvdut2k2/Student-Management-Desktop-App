using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class CourseClassroom_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static CourseClassroom_BLL _instance;

        public static CourseClassroom_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseClassroom_BLL();
                }

                return _instance;
            }
            private set { }
        }

        public List<CourseClassroom> GetAllClassroomOfCourse(string courseId)
        {
            List<CourseClassroom> courseClassrooms = _context.CourseClassroom.Where(courseClass => courseClass.courseId == courseId).ToList();
            return courseClassrooms;
        }
    }
}
