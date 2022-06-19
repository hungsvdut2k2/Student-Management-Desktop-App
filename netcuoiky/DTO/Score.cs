using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class Score
    {
        public int Id { get; set; }
        public User user { get; set; }
        public string userId { get; set; }
        public CourseClassroom courseClassroom { get; set; }
        public string courseClassroomId { get; set; }
        public double? excerciseScore { get; set; }
        public double? midTermScore { get; set; }
        public double? finalTermScore { get; set; }
        public double excerciseRate { get; set; }
        public double midTermRate { get; set; }
        public double finalTermRate { get; set; }

        public double? calScore()
        {
            if (excerciseScore != null && midTermScore != null && finalTermScore != null)
            {
                return excerciseRate * excerciseScore + midTermRate * midTermScore + finalTermRate * finalTermScore;
            }

            return null;
        }
    }
}
