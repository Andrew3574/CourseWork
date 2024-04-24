using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.Models.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.Furnitures
{
    public class KitchenTable : Furniture, IKitchen, IDelcom
    {
        public KitchenTable(string furnitureName, decimal furniturePrice, int furnitureQuantity)
            : base(furnitureName, furniturePrice, furnitureQuantity) { }

        public override string FurnitureVariety { get { return "Стол"; } }

        public override string TypeName { get { return "Кухонная"; } }
        public override decimal TypeMarkup { get { return (decimal)0.15; } }
        public override string ManufacturerName { get { return "ООО «ДЕЛКОМ40»"; } }
        public override decimal ManufacturerMarkup { get { return (decimal)0.05; } }

        public override string FurnitureImage { get { return @"D:\КурсоваяРабота\Images\KitchenTable.png"; } }

        public override decimal GetRetailPrice()
        {
            return FurniturePrice + FurniturePrice * TypeMarkup + FurniturePrice * ManufacturerMarkup;
        }
    }
}
