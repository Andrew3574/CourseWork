using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.UserModels
{
    public class Admin : User
    {
        public Admin(int userId, int roleId, string userName, string password) : base(userId, 1, userName, password) { }
    
        public override string RoleName { get { return "Admin"; } }
    }
}
