using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureSetItems
{
    public class SpecificSetItems : SetItems
    {
        public SpecificSetItems(List<Furniture> furnitures)
            : base(furnitures) { }

        public override string SetName { get { return "Особый"; } }
    }

    
}
