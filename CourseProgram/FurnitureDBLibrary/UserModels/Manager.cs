using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.UserModels
{
    public class Manager : User
    {
        public Manager(string userName, string password) : base(userName, password) { }

        public override string RoleName { get { return "Manager"; } }
    }
}
