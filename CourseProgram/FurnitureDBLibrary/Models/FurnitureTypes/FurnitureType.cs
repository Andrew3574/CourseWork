using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public abstract class FurnitureType
    {
        private string _typeName;
        private decimal _typeMarkup;

        public FurnitureType(string typeName, decimal typeMarkup)
        {
            _typeName = typeName;
            _typeMarkup = typeMarkup;
        }

        public string TypeName { get { return _typeName; } }
        public decimal TypeMarkup { get { return _typeMarkup; } set { _typeMarkup = value; } }

    }
}
