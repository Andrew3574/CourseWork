using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.Manufacturers
{
    public class Delcom : Manufacturer
    {
        public Delcom(string manufacturerName, decimal manufacturerMarkup) : base(manufacturerName,manufacturerMarkup) { }
    }
}
