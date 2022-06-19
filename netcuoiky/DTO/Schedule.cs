using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class Schedule
    {
        public int Id { get; set; }
        public int DateInWeek { get; set; }
        public int StartPeriod { get; set; }
        public int EndPeriod { get; set; }
        public CourseClassroom CourseClassroom { get; set; }
        public string CourseClassId    { get; set; }

        public string ConvertNumberToDate()
        {
            var WeekDays = new Dictionary<int, string>()
            {
                {1, "Thứ Hai"},
                {2, "Thứ Ba"},
                {3, "Thứ Tư"},
                {4, "Thứ Năm"},
                {5, "Thứ Sáu"},
                {6, "Thứ Bảy"},
                {7, "Chủ Nhật"}
            };
            return WeekDays[DateInWeek];
        }

        public static int ConvertDateToNumber(string Date)
        {
            var WeekDays = new Dictionary<string, int>()
            {
                {"Thứ Hai", 1},
                {"Thứ Ba", 2},
                {"Thứ Tư", 3},
                {"Thứ Năm", 4},
                {"Thứ Sáu", 5},
                {"Thứ Bảy", 6},
                {"Chủ Nhật", 7},
            };
            return WeekDays[Date];
        }
    }
}
