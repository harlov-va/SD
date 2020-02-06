using PW.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Models
{
    public class UsersViewModel
    {
        public List<usersModel> Users { get; set; }
        public UsersViewModel()
        {
            this.Users = new List<usersModel>();
        }

        public class usersModel
        {
            int id { get; set; }
            string userName { get; set; }
            string password { get; set; }
            string email { get; set; }
            decimal balance { get; set; }
            DateTime dateTime { get; set; }
        }
    }
}