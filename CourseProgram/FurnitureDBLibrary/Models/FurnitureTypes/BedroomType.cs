using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureTypes
{
    public class BedroomType : FurnitureType
    {
        public BedroomType(string typeName, decimal typeMarkup) : base(typeName, typeMarkup) { }
    }
}
