using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;
using netcuoiky.BLL;
using netcuoiky.DTO.ReturnedDto;

namespace netcuoiky.BLL
{
    public class Schedule_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static Schedule_BLL _instance;

        public static Schedule_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Schedule_BLL();
                }
                return _instance;
            }
            private set { }
        }

        public void AddSchedule(Schedule newSchedule)
        {
            _context.Schedule.Add(newSchedule);
            _context.SaveChanges();
        }
        public List<ReturnedSchedule> GetAllCourseInTheWeek(string teacherId)
        {
            List<ReturnedSchedule> result = new List<ReturnedSchedule>();
            List<UserCourseClassroom> userCourseClassrooms = _context.UserCourseClassroom.Where(ucc => ucc.UserId == teacherId).ToList();
            List<string> courseClassID = new List<string>();
            foreach(var item in userCourseClassrooms)
            {
                courseClassID.Add(item.CourseClassroomId);
            }
            foreach(var item in courseClassID)
            {

                string courseID = CourseClassroom_BLL.Instance.GetCourseIdByCouresClassId(item);
                Course course = _context.Course.Where(c => c.courseId == courseID).FirstOrDefault();
                Schedule schedule = _context.Schedule.Where(s => s.CourseClassId == item).FirstOrDefault();
                ReturnedSchedule returnedSchedule = new ReturnedSchedule {
                    CourseName = course.name,
                    Day = schedule.DateInWeek.ToString(),
                    Time = Convert.ToString(schedule.StartPeriod) + "-" + Convert.ToString(schedule.EndPeriod),
                };
                result.Add(returnedSchedule);
            }
            //Schedule schedule = _context.Schedule.Where(s => s.CourseClassId == item.CourseClassroomId).FirstOrDefault();
            //returnedSchedule.Day = schedule.DateInWeek.ToString();
            //returnedSchedule.Time = Convert.ToString(schedule.StartPeriod) + "-" + Convert.ToString(schedule.EndPeriod);
            return result;
        }
    }
}
