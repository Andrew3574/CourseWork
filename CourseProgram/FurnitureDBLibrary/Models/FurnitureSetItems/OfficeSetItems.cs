using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.Furnitures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureSetItems
{
    public class OfficeSetItems : SetItems
    {
        public OfficeSetItems(List<Furniture> furnitures)
            : base(furnitures) { }

        public override string SetName { get { return "Офисный"; } }
    }
}
