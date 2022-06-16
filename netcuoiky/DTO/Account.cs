using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcuoiky.DTO
{
    public class Account
    {
        public int id { get; set; }
        public string username { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string role { get; set; }
        public User user { get; set; }
        public string userId { get; set; }
    }
}
