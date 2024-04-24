using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.Models.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.CurrentFurnitures
{
    public class OfficeTable : Furniture, IOffice, IRechicadrev
    {
        public OfficeTable(string furnitureName, decimal furniturePrice, int furnitureQuantity)
            : base(furnitureName, furniturePrice, furnitureQuantity) { }

        public override string FurnitureVariety { get { return "Стол"; } }

        public override string TypeName { get { return "Офисная"; } }
        public override decimal TypeMarkup { get { return (decimal)0.10; } }
        public override string ManufacturerName { get { return "ОАО «Речицадрев»"; } }
        public override decimal ManufacturerMarkup { get { return (decimal)0.06; } }

        public override string FurnitureImage { get { return @"D:\КурсоваяРабота\Images\OfficeTable.png"; } }

        public override decimal GetRetailPrice()
        {
            return FurniturePrice + FurniturePrice * TypeMarkup + FurniturePrice * ManufacturerMarkup;
        }
    }
}
