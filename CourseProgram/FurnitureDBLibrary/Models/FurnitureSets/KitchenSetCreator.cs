using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.FurnitureSetItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.Furnitures
{
    public class KitchenSetCreator : FurnitureSetCreator
    {
        public override List<Furniture> Creator(List<Furniture> furnitures)
        {
            var kitchenFurnitures = furnitures.FindAll(f => f.TypeName == "Кухонная");
            return kitchenFurnitures;
        }


    }
}
