using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Users
    {
        private int userId;
        private string username;
        private string password;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        
    }
}
