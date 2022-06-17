using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class Faculty_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static Faculty_BLL _instance;

        public static Faculty_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Faculty_BLL();
                }
                return _instance;
            }
            private set {}
        }

        public Faculty GetFacultyById(string facultyId)
        {
            Faculty faculty = _context.Faculty.Find(facultyId);
            return faculty;
        }
        public List<Faculty> GetAllFaculties()
        {
            List<Faculty> faculties = _context.Faculty.ToList();
            return faculties;
        }
        public List<ComboboxItem> GetComboBoxItems()
        {
            List<ComboboxItem> data = new List<ComboboxItem>();
            foreach (var faculty in Faculty_BLL.Instance.GetAllFaculties())
            {
                if (faculty.name != "Admin")
                {
                    var item = new ComboboxItem
                    {
                        Text = faculty.name,
                        Value = faculty.facultyId
                    }; 
                    data.Add(item);
                }
            }
            return data;
        }
    }
}
