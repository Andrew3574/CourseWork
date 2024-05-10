using FurnitureDBLibrary.Models.FurnitureSetItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureSets
{
    public class LoungeSetCreator : FurnitureSetCreator
    {
        public override List<Furniture> Creator(List<Furniture> furnitures)
        {
            var loungeFurnitures = furnitures.FindAll(f => f.TypeName == "Гостиная");
            return loungeFurnitures;
        }
    }
}
