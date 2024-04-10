using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public abstract class User
    {
        private int _userId;
        private int _roleId;
        private string _userName;
        private string _password;
        
        public User(int userId, int roleId,string userName, string password)
        {
            _userId = userId;
            _roleId = roleId;
            _userName = userName;
            _password = password;
        }

        public int UserId { get { return _userId; } }
        public int RoleId { get { return _roleId; } }
        public string UserName { get { return _userName; } }
        public string Password { get { return _password; } }
        public abstract string RoleName { get; }
        
    }
}
