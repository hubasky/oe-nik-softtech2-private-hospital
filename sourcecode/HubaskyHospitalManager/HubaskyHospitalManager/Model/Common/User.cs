using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Model.Common
{
    public class User
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public Employee Employee { get; private set; }

        public User(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }

        public static User AuthenticateUser(User user)
        {
            User dbUser = null;

            // for testing
            dbUser = user;
            

            return dbUser;
        }

    }
}
