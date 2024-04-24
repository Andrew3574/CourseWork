using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureTypes
{
    public class OfficeType : FurnitureType
    {
        public OfficeType(string typeName, decimal typeMarkup) : base(typeName, typeMarkup) { }
    }
}
