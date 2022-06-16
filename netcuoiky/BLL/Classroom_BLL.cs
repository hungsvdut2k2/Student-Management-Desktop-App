using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class Classroom_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static Classroom_BLL _instance;
        public static Classroom_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Classroom_BLL();
                }
                return _instance;
            }
            private set { }
        }

        public List<ComboboxItem> GetClassByFaculty(string facultyId)
        {
            List<Classroom> classes = _context.Classroom.Where(w => w.facultyId == facultyId).ToList();
            List<ComboboxItem> data = new List<ComboboxItem>();
            foreach (var classroom in classes)
            {
                var item = new ComboboxItem
                {
                    Text = classroom.name,
                    Value = classroom.classId
                };
                data.Add(item);
            }
            return data;
        }
    }
}
