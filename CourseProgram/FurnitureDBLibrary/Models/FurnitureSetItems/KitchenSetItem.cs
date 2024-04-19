using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class KitchenSetItem : FurnitureSetItem
    {
        public KitchenSetItem(string furnitureName, decimal furniturePrice, int furnitureQuantity, FurnitureType furnitureType, Manufacturer furnitureManufacturerName) 
            : base(furnitureName, furniturePrice, furnitureQuantity, furnitureType, furnitureManufacturerName) { }

        public override string FurnitureVariety { get; }
        public override string FurnitureSetName { get { return "Кухонный"; } }
    }
}
