using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public abstract class Manufacturer
    {
        private string _manufacturerName;
        private decimal _manufacturerMarkup;

        public Manufacturer(string manufacturerName, decimal manufacturerMarkup)
        {
            _manufacturerName = manufacturerName;
            _manufacturerMarkup = manufacturerMarkup;
        }

        public string ManufacturerName { get { return _manufacturerName; } }    
        public decimal ManufacturerMarkup { get { return _manufacturerMarkup; } }

    }
}
