using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;
using netcuoiky.DTO.ReturnedDto;

namespace netcuoiky.BLL
{
    public class Score_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static Score_BLL _instance;

        public static Score_BLL Instance
        {
            get
            {
                if( _instance == null )
                    _instance = new Score_BLL();
                return _instance;
            }
            private set{}
        }

        public List<ReturnedScore> GetAllScoreOfStudent(string userId)
        {
            List<ReturnedScore> resList = new List<ReturnedScore>();
            List<Score> scoreList = _context.Score.Where(score => score.userId == userId).ToList();
            foreach (var score in scoreList)
            {
                CourseClassroom courseClassroom = _context.CourseClassroom.Find(score.courseClassroomId);
                Course course = _context.Course.Find(courseClassroom.courseId);
                var item = new ReturnedScore
                {
                    courseClassId = score.courseClassroomId,
                    courseName = course.name,
                    Credits = course.Credits,
                    excerciseScore = score.excerciseScore,
                    midTermScore = score.midTermScore,
                    finalTermScore = score.finalTermScore,
                    courseScoreRate =
                        $"{score.excerciseRate} * BT + {score.midTermRate} * GK + {score.finalTermRate} * CK ",
                    averageScore = score.calScore()
                };
                resList.Add(item);
            }
            return resList;
        }

        public List<ReturnedScoreOfStudent> GetScoreOfCourseClass(string courseClassId)
        {
            List<ReturnedScoreOfStudent> resList = new List<ReturnedScoreOfStudent>();
            List<Score> scoreList = _context.Score.Where(score => score.courseClassroomId == courseClassId).ToList();
            foreach (var score in scoreList)
            {
                CourseClassroom courseClassroom = _context.CourseClassroom.Find(score.courseClassroomId);
                Course course = _context.Course.Find(courseClassroom.courseId);
                User student = User_BLL.Instance.GetUser(score.userId);
                var item = new ReturnedScoreOfStudent
                {
                    studentId = student.userId,
                    studentName = student.name,
                    excerciseScore = score.excerciseScore,
                    midTermScore = score.midTermScore,
                    finalTermScore = score.finalTermScore,
                    averageScore = score.calScore()
                };
                resList.Add(item);
            }
            return resList;
        }

        public void AddScore(Score score)
        {
            _context.Score.Add(score);
            _context.SaveChanges();
        }
        public void DeleteScore(string userId)
        {
            List<Score> scoreList = _context.Score.Where(score => score.userId == userId).ToList();
            _context.Score.RemoveRange(scoreList);
            _context.SaveChanges();
        }

        public void UpdateScore(string userId, string courseClassId, Score tempScore)
        {
            Score findingScore = _context.Score.Where(score => score.userId == userId && score.courseClassroomId == courseClassId).FirstOrDefault();
            if (findingScore != null)
            {
                findingScore.excerciseScore = tempScore.excerciseScore;
                findingScore.midTermScore = tempScore.midTermScore;
                findingScore.finalTermScore = tempScore.finalTermScore;
                _context.SaveChanges();
            }
        }

    }
}
