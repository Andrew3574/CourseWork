using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class Manager : User
    {
        public Manager(int userId, int roleId, string userName, string password) : base(userId, 3, userName, password) { }

        public override string RoleName { get { return "Manager"; } }
    }
}
