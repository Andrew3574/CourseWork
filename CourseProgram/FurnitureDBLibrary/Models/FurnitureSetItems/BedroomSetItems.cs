using FurnitureDBLibrary.Models.CurrentFurnitures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureSetItems
{
    public class BedroomSetItems : SetItems
    {
        public BedroomSetItems(List<Furniture> furnitures)
            : base(furnitures) { }

        public override string SetName { get { return "Спальный"; } }
    }
}
