using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.Furnitures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class KitchenSetItems : SetItems
    {
        public KitchenSetItems(List<Furniture> furnitures)
            : base(furnitures) { }

        public override string SetName { get { return "Кухонный"; } }
    }
}
