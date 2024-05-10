using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class Sale
    {
        private string _furnitureName;
        private decimal _furnitureRetailPrice;
        private string _manufacturerName;
        private string _typeName;
        private int _furnitureSaledQuantity;
        private DateTime _saleDate;

        public Sale(string furnitureName, decimal furnitureRetailPrice,string manufacturerName,string typeName, int furnitureSaledQuantity, DateTime saleDate)
        {
            _furnitureName = furnitureName;
            _furnitureRetailPrice = furnitureRetailPrice;
            _manufacturerName = manufacturerName;
            _typeName = typeName;
            _furnitureSaledQuantity = furnitureSaledQuantity;
            _saleDate = saleDate;

        }

        public string FurnitureName { get { return _furnitureName; } }
        public decimal FurnitureRetailPrice { get { return _furnitureRetailPrice; } }
        public string ManufacturerName { get { return _manufacturerName; } }   
        public string TypeName { get { return _typeName; } }
        public int FurnitureSaledQuantity { get { return _furnitureSaledQuantity; } set { _furnitureSaledQuantity = value; } }
        public DateTime SaleDate { get { return _saleDate; } }

        public decimal TotalCost
        {
            get
            {
                return FurnitureRetailPrice * _furnitureSaledQuantity;
            }
        }

        public override string ToString()
        {
            return $"{FurnitureName} | {FurnitureRetailPrice:#.00} | {ManufacturerName} | {TypeName} | {FurnitureSaledQuantity} | {TotalCost} | {SaleDate:d}";
        }
    }
}
