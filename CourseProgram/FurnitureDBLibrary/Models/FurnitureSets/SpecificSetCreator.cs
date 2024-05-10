using FurnitureDBLibrary.Models.FurnitureSetItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureSets
{
    public class SpecificSetCreator : FurnitureSetCreator
    {
        public override List<Furniture> Creator(List<Furniture> furnitures)
        {
            var specificFurnitures = furnitures.FindAll(f => f.TypeName == "Особая");
            return specificFurnitures;
        }
    }
}
