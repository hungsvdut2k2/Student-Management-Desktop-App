using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using netcuoiky.DAL;
using netcuoiky.DTO;
using netcuoiky.DTO.ReturnedDto;

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

        public Account GetAccountByUserId(string userId)
        {
            Account findingAccount = _context.Account.Where(w => w.userId == userId).FirstOrDefault();
            return findingAccount;
        }

        public void DeleteAccount(string userId)
        {
            Account findingAccount = _context.Account.Where(w => w.userId == userId).FirstOrDefault();
            _context.Account.Remove(findingAccount);
            _context.SaveChanges();
        }

        public void ChangePassword(string password, string email)
        {
            User user = _context.User.Where(w => w.email == email).FirstOrDefault();
            Account account = GetAccountByUserId(user.userId);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            account.passwordHash = passwordHash;
            account.passwordSalt = passwordSalt;
            _context.SaveChanges();
        }
        private string randomPassword()
        {
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < 10; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            return str_build.ToString();
        }

        public List<ReturnedAccount> RegisterAccounts(List<RegisterDto> request)
        {
            List<Account> accounts = new List<Account>();
            List<User> userInformations = new List<User>();
            List<ReturnedAccount> result = new List<ReturnedAccount>();
            foreach (var item in request)
            {
                Classroom classroom = _context.Classroom.Find(item.className);
                if (classroom != null)
                {
                    var user = new User
                    {
                        userId = item.userId,
                        name = item.studentName,
                        dob = item.Dob,
                        phoneNumber = item.phoneNumber,
                        email = item.Email,
                        gender = item.Gender,
                        classroom = null,
                        classId = classroom.classId
                    };
                    string password = randomPassword();
                    CreatePasswordHash(Convert.ToString(password), out byte[] passwordHash, out byte[] passwordSalt);
                    var account = new Account
                    {
                        username = item.userId,
                        passwordHash = passwordHash,
                        passwordSalt = passwordSalt,
                        user = null,
                        userId = item.userId,
                        role = item.Role
                    };
                    accounts.Add(account);
                    userInformations.Add(user);
                    var newAccount = new ReturnedAccount
                    {
                        userId = item.userId,
                        studentName = item.studentName,
                        userName = item.userId,
                        password = password
                    };
                    result.Add(newAccount);
                }
            }

            _context.User.AddRange(userInformations);
            _context.Account.AddRange(accounts);
            _context.SaveChanges();
            return result;
        }
    }
}
