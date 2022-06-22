using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public List<ReturnedCourse> GetAllCourseInEducationalProgram(string educationalProgramId)
        {
            List<CourseEducationalProgram> courseEducational =
                _context.CourseEducationalProgram.Where(w => 
                    w.EducationalProgramId == educationalProgramId).ToList();
            List<ReturnedCourse> courseList = new List<ReturnedCourse>();
            foreach (var item in courseEducational)
            {
                Course course = _context.Course.Find(item.CourseId);
                ReturnedCourse res = new ReturnedCourse
                {
                    courseId = course.courseId,
                    CourseName = course.name,
                    Credits = course.Credits,
                    Semester = item.Semester,
                    requiremnetId = course.requirementId,
                    isAvailable = course.IsAvailable
                };
                courseList.Add(res);
            }

            return courseList;
        }

        public List<ComboboxItem> GetAllEducationalProgram()
        {
            List<EducationalProgram> educationalPrograms = _context.EducationalProgram.ToList();
            List<ComboboxItem> data = new List<ComboboxItem>();
            foreach (var educationalProgram in educationalPrograms)
            {
                if (educationalProgram.EducationalProgramName != "Admin")
                {
                    var item = new ComboboxItem
                    {
                        Text = educationalProgram.EducationalProgramName,
                        Value = educationalProgram.EducationalProgramId
                    };
                    data.Add(item);
                }
                
            }
            return data;
        }

        public void AddCourseToEducationalProgram(CourseEducationalProgram item)
        {
            try
            {
                _context.CourseEducationalProgram.Add(item);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public void DeleteCourseInEducationalProgram(string courseId, string educationalProgramId)
        {
            CourseEducationalProgram courseEducationalProgram = _context.CourseEducationalProgram.Where(w =>
                w.EducationalProgramId == educationalProgramId && w.CourseId == courseId).FirstOrDefault();
            _context.CourseEducationalProgram.Remove(courseEducationalProgram);
            _context.SaveChanges();
        }

        public void UpdateEducationalProgram(string courseId, string educationalProgramId, CourseEducationalProgram temProgram)
        {
            CourseEducationalProgram courseEducationalProgram = _context.CourseEducationalProgram.Where(w =>
                w.EducationalProgramId == educationalProgramId && w.CourseId == courseId).FirstOrDefault();
            courseEducationalProgram.Semester = temProgram.Semester;
            _context.SaveChanges();
        }

        public EducationalProgram GetEducationalProgramById(string educationProgramId)
        {
            EducationalProgram educationalProgram = _context.EducationalProgram.Find(educationProgramId);
            return educationalProgram;
        }
        public List<ReturnedCourse> SortListCourse(List<ReturnedCourse> courses)
        {
            return courses.OrderBy(o => o.Semester).ToList();
        }
        public List<ReturnedCourse> SortListCourseDesc(List<ReturnedCourse> courses)
        {
            return courses.OrderByDescending(o => o.Semester).ToList();
        }
    }
}
