using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.CurrentFurnitures
{
    public class Chair : Furniture
    {
        public Chair(string furnitureName,decimal furniturePrice, int furnitureQuantity, FurnitureType furnitureType, Manufacturer furnitureManufacturerName) 
            : base(furnitureName,furniturePrice,furnitureQuantity,furnitureType,furnitureManufacturerName) { }

        public override string FurnitureVariety { get { return "Стул"; } }

    }
}
