using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureSetItems
{
    public abstract class FurnitureSetCreator
    {
        public abstract List<Furniture> Creator(List<Furniture> furnitures);
    }
}
