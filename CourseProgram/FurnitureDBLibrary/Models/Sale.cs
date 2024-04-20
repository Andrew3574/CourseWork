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
        private decimal _furniturePrice;
        private FurnitureType _furnitureType;
        private Manufacturer _furnitureManufacturer;
        private int _furnitureSaledQuantity;
        private DateTime _saleDate;

        public Sale(string furnitureName, decimal furniturePrice, FurnitureType furnitureType, Manufacturer furnitureManufacturer, int furnitureSaledQuantity, DateTime saleDate)
        {
            _furnitureName = furnitureName;
            _furniturePrice = furniturePrice;
            _furnitureType = furnitureType;
            _furnitureManufacturer = furnitureManufacturer;
            _furnitureSaledQuantity = furnitureSaledQuantity;
            _saleDate = saleDate;

        }

        public string FurnitureName { get { return _furnitureName; } }
        public decimal FurniturePrice { get { return _furniturePrice; } }
        public FurnitureType FurnitureType { get { return _furnitureType; } }
        public Manufacturer FurnitureManufacturer { get { return _furnitureManufacturer; } }
        public int FurnitureSaledQuantity { get { return _furnitureSaledQuantity; } set { _furnitureSaledQuantity = value; } }
        public DateTime SaleDate { get { return _saleDate; } }

        public decimal GetTotalCost()
        {
            decimal totalCost = 0;
            totalCost = (FurniturePrice + FurniturePrice * FurnitureType.TypeMarkup + FurniturePrice * FurnitureManufacturer.ManufacturerMarkup) * _furnitureSaledQuantity;

            return totalCost;
        }

        public override string ToString()
        {
            return $"{FurnitureName} | {FurniturePrice:#.00} | {FurnitureManufacturer.ManufacturerName} | {FurnitureType.TypeName} | {FurnitureSaledQuantity} | {GetTotalCost()} | {SaleDate:d}";
        }
    }
}
