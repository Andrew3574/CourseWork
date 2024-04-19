using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.UserModels
{
    public abstract class User
    {        
        private string _userName;
        private string _password;

        public User(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public string UserName { get { return _userName; } }
        public string Password { get { return _password; } }
        public abstract string RoleName { get; }
    }
}
