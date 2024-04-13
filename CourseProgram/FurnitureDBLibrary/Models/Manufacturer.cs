using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class Manufacturer
    {
        private short _manufacturerId;
        private string _manufacturerName;
        private decimal _manufacturerMarkup;

        public Manufacturer(short manufacturerId, string manufacturerName, decimal manufacturerMarkup)
        {
            _manufacturerId = manufacturerId;
            _manufacturerName = manufacturerName;
            _manufacturerMarkup = manufacturerMarkup;
        }

        public short ManufacturerId { get { return _manufacturerId; } } 
        public string ManufacturerName { get { return _manufacturerName; } }    
        public decimal ManufacturerMarkup { get { return _manufacturerMarkup; } }

        public override string ToString()
        {
            return $"{ManufacturerName}";
        }


    }
}
