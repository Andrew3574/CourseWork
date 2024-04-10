using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class Furniture
    {
        private int _furnitureId;
        private string _furnitureName;
        private decimal _furniturePrice;
        private int _furnitureQuantity;
        private short _furnitureTypeId;
        private short _furnitureManufacturerId;

        public Furniture(int furnitureId, string furnitureName, decimal furniturePrice, int furnitureQuantity, short furnitureType, short furnitureManufacturerName)
        {
            _furnitureId = furnitureId;
            _furnitureName = furnitureName;
            _furniturePrice = furniturePrice;
            _furnitureQuantity = furnitureQuantity;
            _furnitureTypeId = furnitureType;
            _furnitureManufacturerId = furnitureManufacturerName;
        }

        public int FurnitureId { get { return _furnitureId; } }
        public string FurnitureName { get {  return _furnitureName; } } 
        public decimal FurniturePrice { get {  return _furniturePrice; } }  
        public int FurnitureQuantity { get {  return _furnitureQuantity; } }    
        public short FurnitureTypeId { get {  return _furnitureTypeId; } } 
        public short FurnitureManufacturerId { get { return _furnitureManufacturerId; } }
    
    }
}
