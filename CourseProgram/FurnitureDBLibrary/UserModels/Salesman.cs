using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.UserModels
{
    public class Salesman : User
    {
        public Salesman(int userId, int roleId, string userName, string password) : base(userId, 2, userName, password) { }

        public override string RoleName { get { return "Salesman"; } }
    }
}
