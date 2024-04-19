using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.UserModels
{
    public class Salesman : User
    {
        public Salesman(string userName, string password) : base(userName, password) { }

        public override string RoleName { get { return "Salesman"; } }
    }
}
