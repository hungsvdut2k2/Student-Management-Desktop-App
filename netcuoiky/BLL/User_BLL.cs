using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class User_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static User_BLL _instance;

        public static User_BLL Instance
        {
            get
            {
                if( _instance == null )
                    _instance = new User_BLL();
                return _instance;
            }
            private set {}
        }

        public User GetUser(string userdId)
        {
            User user = _context.User.Find(userdId);
            return user;
        }
        public List<User> GetAllUserInClass(string userId)
        {
            User user = GetUser(userId);
            List<User> users = _context.User.Where(w => w.classId == user.classId).ToList();
            return users;
        }
    }
}
