using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class Sale
    {
        private int _saleId;
        private int _furnitureId;
        private int _furnitureSaledQuantity;
        private DateTime _saleDate;

        public Sale(int saleId, int furnitureId, int furnitureSaledQuantity, DateTime saleDate)
        {
            _saleId = saleId;
            _furnitureId = furnitureId;
            _furnitureSaledQuantity = furnitureSaledQuantity;
            _saleDate = saleDate;
        }

        public int SaleId { get { return _saleId; } }
        public int FurnitureId { get { return _furnitureId; } }
        public int FurnitureSaledQuantity { get { return _furnitureSaledQuantity; } }
        public DateTime SaleDate { get { return _saleDate; } }
    
    
    }
}
