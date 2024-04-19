using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public abstract class FurnitureSetItem : Furniture
    {
        public FurnitureSetItem(string furnitureName, decimal furniturePrice, int furnitureQuantity, FurnitureType furnitureType, Manufacturer furnitureManufacturerName)
            : base(furnitureName, furniturePrice, furnitureQuantity, furnitureType, furnitureManufacturerName) { }

        public abstract string FurnitureSetName { get; }   

    }
}
