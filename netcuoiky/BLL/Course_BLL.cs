using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class Course_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static Course_BLL _instance;

        public static Course_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Course_BLL();
                }

                return _instance;
            }
            private set { }
        }

        public List<ComboboxItem> GetAllCourse()
        {
            List<Course> courses = _context.Course.ToList();
            List<ComboboxItem> data = new List<ComboboxItem>();
            foreach (var course in courses)
            {
                var item = new ComboboxItem
                {
                    Text = course.name,
                    Value = course.courseId
                };
                data.Add(item);
            }

            return data;
        }

        public Course GetCourseById(string courseId)
        {
            Course course = _context.Course.Find(courseId);
            return course;
        }

        public void AddCourse(Course newCourse)
        {
            _context.Course.Add(newCourse);
            _context.SaveChanges();
        }
    }
}
