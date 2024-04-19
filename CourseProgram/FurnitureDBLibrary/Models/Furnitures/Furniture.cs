using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public abstract class Furniture
    {
        private string _furnitureName;
        private decimal _furniturePrice;
        private int _furnitureQuantity;
        private FurnitureType _furnitureType;
        private Manufacturer _furnitureManufacturer;

        public Furniture(string furnitureName, decimal furniturePrice, int furnitureQuantity, FurnitureType furnitureType, Manufacturer furnitureManufacturerName)
        {
            _furnitureName = furnitureName;
            _furniturePrice = furniturePrice;
            _furnitureQuantity = furnitureQuantity;
            _furnitureType = furnitureType;
            _furnitureManufacturer = furnitureManufacturerName;
        }

        public string FurnitureName { get { return _furnitureName; } }
        public decimal FurniturePrice { get { return _furniturePrice; } }
        public int FurnitureQuantity { get { return _furnitureQuantity; } set { _furnitureQuantity = value; } }
        public FurnitureType FurnitureType { get { return _furnitureType; } }
        public Manufacturer FurnitureManufacturer { get { return _furnitureManufacturer; } }
        public abstract string FurnitureVariety { get; }        

    }
}
