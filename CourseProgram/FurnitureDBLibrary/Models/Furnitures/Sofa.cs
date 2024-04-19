using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.CurrentFurnitures
{
    public class Sofa : Furniture
    {
        public Sofa(string furnitureName, decimal furniturePrice, int furnitureQuantity, FurnitureType furnitureType, Manufacturer furnitureManufacturerName) 
            : base(furnitureName, furniturePrice, furnitureQuantity, furnitureType, furnitureManufacturerName) { }   
        
         public override string FurnitureVariety { get { return "Диван"; } }
    }
}
