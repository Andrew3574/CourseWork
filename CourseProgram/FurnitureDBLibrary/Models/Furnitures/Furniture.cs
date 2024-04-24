using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.Models.Manufacturers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public abstract class Furniture : IType, IManufacturer
    {
        private string _furnitureName;
        private decimal _furniturePrice;
        private int _furnitureQuantity;
        
        public Furniture(string furnitureName, decimal furniturePrice, int furnitureQuantity)
        {
            _furnitureName = furnitureName;
            _furniturePrice = furniturePrice;
            _furnitureQuantity = furnitureQuantity;
        } 

        public string FurnitureName { get { return _furnitureName; } }
        public decimal FurniturePrice { get { return _furniturePrice; } }
        public int FurnitureQuantity { get { return _furnitureQuantity; } set { _furnitureQuantity = value; } }

        public override string ToString()
        {
            return $"{FurnitureName}\t{GetRetailPrice():#.00}";
        }

        public abstract string TypeName { get; }
        public abstract decimal TypeMarkup { get; }
        public abstract string ManufacturerName { get; }
        public abstract decimal ManufacturerMarkup { get; }
        public abstract string FurnitureVariety { get; }
        public abstract string FurnitureImage { get; }

        public abstract decimal GetRetailPrice();
    }
}
