using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.Models.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.Furnitures
{
    public class OfficeChair : Furniture, IOffice, IRechicadrev
    {
        public OfficeChair(string furnitureName, decimal furniturePrice, int furnitureQuantity)
            : base(furnitureName, furniturePrice, furnitureQuantity) { }

        public override string FurnitureVariety { get { return "Стул"; } }


        public override string TypeName { get; set; }
        public override decimal TypeMarkup { get; set; }
        public override string ManufacturerName { get; set; }
        public override decimal ManufacturerMarkup { get; set; }
        public override string FurnitureImage { get; set; }
        public override decimal GetRetailPrice()
        {
            return FurniturePrice + FurniturePrice * TypeMarkup + FurniturePrice * ManufacturerMarkup;
        }
    }
}
