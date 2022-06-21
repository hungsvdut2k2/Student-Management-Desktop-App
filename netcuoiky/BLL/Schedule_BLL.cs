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

        public bool CheckConflictSchedule(string userId, string courseClassId)
        {
            CourseClassroom findingCourseClassroom = _context.CourseClassroom.Find(courseClassId);
            List<Schedule> findingSchedules = _context.Schedule
                .Where(w => w.CourseClassId == findingCourseClassroom.CourseClassId).ToList();
            List<CourseClassroom> courseClassrooms = new List<CourseClassroom>();
            List<UserCourseClassroom> userCourseClassrooms = _context.UserCourseClassroom.Where(w => w.UserId == userId).ToList();
            foreach (var item in userCourseClassrooms)
            {
                var courseClassroom = _context.CourseClassroom.Find(item.CourseClassroomId);
                courseClassrooms.Add(courseClassroom);
            }
            List<Schedule> schedules = new List<Schedule>();
            foreach (var courseClassroom in courseClassrooms)
            {
                List<Schedule> tempSchedules =
                    _context.Schedule.Where(s => s.CourseClassId == courseClassroom.CourseClassId).ToList();
                schedules.AddRange(tempSchedules);
            }

            foreach (var item in findingSchedules)
            {
                foreach (var schedule in schedules)
                {
                    if (item.DateInWeek == schedule.DateInWeek)
                    {
                        if ((item.StartPeriod >= schedule.StartPeriod && item.StartPeriod <= schedule.EndPeriod) ||
                            (item.EndPeriod >= schedule.StartPeriod && item.EndPeriod <= schedule.EndPeriod))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
