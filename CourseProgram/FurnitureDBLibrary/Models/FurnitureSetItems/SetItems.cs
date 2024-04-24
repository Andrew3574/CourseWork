using FurnitureDBLibrary.Models.FurnitureSetItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public abstract class SetItems :  ISet
    {
        private readonly List<Furniture> _furnitures = new List<Furniture>();

        public SetItems(List<Furniture> furnitures)
        {
            _furnitures = furnitures;
        }

        public List<Furniture> FurnitureList { get { return _furnitures; } }

        public abstract string SetName { get; }

        public override string ToString()
        {
            string info = "";
            foreach (var item in _furnitures)
            {
                info += $"{item.FurnitureName}\t{item.GetRetailPrice():#.##}\n";
            }

            return info;
        }
    }
}
