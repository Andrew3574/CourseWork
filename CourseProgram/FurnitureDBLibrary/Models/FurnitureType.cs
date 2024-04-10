using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class FurnitureType
    {
        private short _typeId;
        private string _typeName;
        private decimal _typeMarkup;

        public FurnitureType(short typeId, string typeName, decimal typeMarkup)
        {
            _typeId = typeId;
            _typeName = typeName;
            _typeMarkup = typeMarkup;
        }

        public short TypeId { get { return _typeId; } }
        public string TypeName { get { return _typeName; } }
        public decimal TypeMarkup { get { return _typeMarkup; } }

        
    }
}
