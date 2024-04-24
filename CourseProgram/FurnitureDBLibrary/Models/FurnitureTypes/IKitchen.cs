using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.FurnitureTypes
{
    public interface IKitchen
    {
        string TypeName { get; }

        decimal TypeMarkup { get; }

    }
}
