using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using netcuoiky.DAL;
using netcuoiky.DTO;

namespace netcuoiky.BLL
{
    public class Account_BLL
    {
        private readonly SM_DAL _context = new SM_DAL();
        public static Account_BLL _instance;

        public static Account_BLL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Account_BLL();
                }
                return _instance;
            }
            private set {}
        }

        private Account_BLL()
        {

        }
        public void Register(string userName, string Password, string Role, string UserId, string classId)
        {
            Classroom usingClassroom = _context.Classroom.Where(classroom => classroom.classId == classId).FirstOrDefault();
            User newUser = new User
            {
                userId = UserId,
                nation = string.Empty,
                gender = true,
                dob = string.Empty,
                birthPlace = string.Empty,
                personalId = string.Empty,
                medicalCode = string.Empty,
                phoneNumber = string.Empty,
                classroom = usingClassroom
            };
            _context.User.Add(newUser);
            _context.SaveChanges();
            CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);
            Account newAccount = new Account
            {
                username = userName,
                passwordHash = passwordHash,
                passwordSalt = passwordSalt,
                role = Role,
                user = newUser
            };
            _context.Account.Add(newAccount);
            _context.SaveChanges();
        }
        public Account Login(string Username, string Password)
        {
            Account loginAccount = _context.Account.Where(account => account.username == Username).FirstOrDefault();
            if (loginAccount == null)
                return null;
            if (Verified(Password, loginAccount.passwordHash, loginAccount.passwordSalt) == true)
                return loginAccount;
            else
            {
                return null;
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool Verified(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
