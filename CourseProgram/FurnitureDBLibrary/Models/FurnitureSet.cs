using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class FurnitureSet
    {
        private int _furnitureSetId;
        private string _furnitureSetName;

        public FurnitureSet(int furnitureSetId, string furnitureSetName)
        {
            _furnitureSetId = furnitureSetId;
            _furnitureSetName = furnitureSetName;
        }

        public int FurnitureSetId { get { return _furnitureSetId; } }
        public string FurnitureSetName { get { return _furnitureSetName; } }
    
    }
}
