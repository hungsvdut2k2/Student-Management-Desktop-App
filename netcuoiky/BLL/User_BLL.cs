﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<User> GetAllUser()
        {
            List<User> users = _context.User.ToList();
            return users;
        }
        public User GetUser(string userdId)
        {
            User user = _context.User.Find(userdId);
            if( user == null )
                return null;
            return user;
        }
        public List<User> GetAllUserInClass(string classId)
        {
            List<User> users = _context.User.Where(w => w.classId == classId).ToList();
            return users;
        }

        public List<User> GetAllUserInFaculty(string facultyId)
        {
            List<Classroom> classrooms = _context.Classroom.Where(w => w.facultyId == facultyId).ToList();
            List<User> resList = new List<User>();
            foreach (var classroom in classrooms)
            {
                List<User> users = _context.User.Where(w => w.classId == classroom.classId).ToList();
                resList.AddRange(users);
            }
            return resList;
        }

        public List<ComboboxItem> GetAllUserInCLassComboboxItems(string classId)
        {
            List<User> users = _context.User.Where(w => w.classId == classId).ToList();
            List<ComboboxItem> data = new List<ComboboxItem>();
            foreach (var user in users)
            {
                var item = new ComboboxItem
                {
                    Text = user.name,
                    Value = user.userId
                };
                data.Add(item);
            }

            return data;
        }

        public void UpdateUser(string userId, User tempUser)
        {
            User user = User_BLL.Instance.GetUser(userId);
            if (user != null)
            {
                user.name = tempUser.name;
                user.birthPlace = tempUser.birthPlace;
                user.medicalCode = tempUser.medicalCode;
                user.dob = tempUser.dob;
                user.phoneNumber = tempUser.phoneNumber;
                user.personalId = tempUser.personalId;
                user.nation = tempUser.nation;
                user.gender = tempUser.gender;
                user.email = tempUser.email;
                _context.SaveChanges();
            }
            return;
        }

        public void DeleteUser(string userId)
        {
            User user = User_BLL.Instance.GetUser(userId);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
            return;
        }

        public bool checkValidMail(string email)
        {
            List<User> users = User_BLL.Instance.GetAllUser();
            foreach (var user in users)
            {
                if (user.email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
