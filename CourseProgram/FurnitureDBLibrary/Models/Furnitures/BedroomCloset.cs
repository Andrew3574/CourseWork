using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.Models.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.CurrentFurnitures
{
    public class BedroomCloset : Furniture, IBedroom, IMozirles
    {
        public BedroomCloset(string furnitureName, decimal furniturePrice, int furnitureQuantity)
            : base(furnitureName, furniturePrice, furnitureQuantity) { }

        public override string FurnitureVariety { get { return "Шкаф"; } }

        public override string TypeName { get { return "Спальная"; } }
        public override decimal TypeMarkup { get { return (decimal)0.15; } }
        public override string ManufacturerName { get { return "ЗАО «Мозырьлес»"; } }
        public override decimal ManufacturerMarkup { get { return (decimal)0.08; } }
        public override string FurnitureImage { get { return @"D:\КурсоваяРабота\Images\OakCloset.png"; } }

        public override decimal GetRetailPrice()
        {
            return FurniturePrice + FurniturePrice * TypeMarkup + FurniturePrice * ManufacturerMarkup;
        }
    }
}
