using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models
{
    public class FurnitureSetItem
    {
        private int _setItemId;
        private int _furnitureSetId;
        private int _furnitureId;

        public FurnitureSetItem(int setItemId, int furnitureSetId, int furnitureId)
        {
            _setItemId = setItemId;
            _furnitureSetId = furnitureSetId;
            _furnitureId = furnitureId;
        }

        public int SetItemId { get { return _setItemId; } }
        public int FurnitureSetId { get {  return _furnitureSetId; } }   
        public int FurnitureId { get { return _furnitureId; } }

    }
}
