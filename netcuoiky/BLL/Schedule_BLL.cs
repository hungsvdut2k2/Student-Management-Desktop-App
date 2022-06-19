using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

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
    }
}
