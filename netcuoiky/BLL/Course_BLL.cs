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

        public void UpdateCourse(string courseId, Course tempCourse)
        {
            Course course = _context.Course.Find(courseId);
            course.courseId = tempCourse.courseId;
            course.name = tempCourse.name;
            course.Credits = tempCourse.Credits;
            course.requirementId = tempCourse.requirementId;
            _context.SaveChangesAsync();
        }
        public bool checkCourseExist(string courseId)
        {
            Course course = _context.Course.Find(courseId);
            if(course == null)
                return false;
            return true;
        }

        public bool checkUncompletedCourse(string userId, string courseId)
        {
            UserCourse completedCourse = _context.UserCourse
                .Where(userCourse => userCourse.CourseId == courseId && userCourse.UserId == userId).FirstOrDefault();
            if (completedCourse == null)
                return true;
            return false;
        }
        public List<ComboboxItem> FilterCourseForStudent(string userId)
        {
            //course in student's educational program
            User user = User_BLL.Instance.GetUser(userId);
            Classroom classroom = Classroom_BLL.Instance.GetClassroomById(user.classId);
            EducationalProgram educationalProgram =
                EducationalProgram_BLL.Instance.GetEducationalProgramById(classroom.educationalProgramId);
            //All Course in Educational Program
            List<ReturnedCourse> courseInEducationalProgram =
                EducationalProgram_BLL.Instance.GetAllCourseInEducationalProgram(
                    educationalProgram.EducationalProgramId);
            //Check available course
            List<ReturnedCourse> availableCourses = new List<ReturnedCourse>();
            foreach (var course in courseInEducationalProgram)
            {
                if (course.isAvailable)
                {
                    availableCourses.Add(course);
                }
            }
            //Check uncompleted course
            List<ReturnedCourse> uncompletedCourse = new List<ReturnedCourse>();
            foreach (var course in availableCourses)
            {
                if (checkUncompletedCourse(userId, course.courseId))
                {
                    uncompletedCourse.Add(course);
                }
            }
            //Check requirement 
            List<ReturnedCourse> finalList = new List<ReturnedCourse>();
            foreach (var course in uncompletedCourse)
            {
                if (course.requiremnetId != null)
                {
                    if (checkUncompletedCourse(userId, course.requiremnetId) == false)
                    {
                        finalList.Add(course);
                    }
                }
                else
                {
                    finalList.Add(course);
                }
            }

            //Turn data into combobox
            List<ComboboxItem> comboboxItems = new List<ComboboxItem>();
            foreach (var course in finalList)
            {
                var item = new ComboboxItem
                {
                    Text = course.CourseName,
                    Value = course.courseId
                };
                comboboxItems.Add(item);
            } 
            return comboboxItems;
        }
    }
}
