using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models.Furnitures;
using FurnitureDBLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FurnitureDBLibrary.Models.FurnitureSetItems;
using System.Collections.Generic;
using FurnitureDBLibrary.Models.FurnitureSets;

namespace FurnitureShopUnitTests
{
    [TestClass]
    public class FurnitureSetTests
    {
        readonly FurnitureSetItemController furnitureSetItemController = new FurnitureSetItemController();
        readonly FurnitureController furnitureController = new FurnitureController();
        
        SetItems newSetItems;

        [TestMethod]
        public void CreateTests()
        {
           /* List<Furniture> furnitures = furnitureController.Read().FindAll(item => item.TypeName == "Офисная");
            SetItems newSetItems = new OfficeSetItems(furnitures);

            if (furnitureSetItemController.Read().Find(setitem => setitem.SetName == "Офисный") != null)
                furnitureSetItemController.Delete(newSetItems);

            furnitureSetItemController.Create(newSetItems);*/

        }

        [TestMethod]
        public void UpdateTest()
        {
            /*List<Furniture> furnitures = furnitureController.Read().FindAll(item => item.TypeName == "Офисная");
            SetItems newSetItems = new OfficeSetItems(furnitures);

            furnitureSetItemController.Create(newSetItems);
            furnitureSetItemController.Update(newSetItems);*/
        }

        [TestMethod]
        public void ReadTest()
        {
           /* List<Furniture> furnitures = furnitureController.Read().FindAll(item => item.TypeName == "Офисная");
            SetItems newSetItems = new OfficeSetItems(furnitures);

            if (furnitureSetItemController.Read().Find(setitem => setitem.SetName == "Офисный") != null)
                furnitureSetItemController.Delete(newSetItems);

            furnitureSetItemController.Create(newSetItems);
            furnitureSetItemController.Read();*/
        }


        [TestMethod]
        public void DeleteTest()
        {
            /*List<Furniture> furnitures = furnitureController.Read().FindAll(item => item.TypeName == "Офисная");
            SetItems newSetItems = new OfficeSetItems(furnitures);

            if (furnitureSetItemController.Read().Find(setitem => setitem.SetName == "Офисный") == null)
            {
                
                furnitureSetItemController.Create(newSetItems); 
            }

            furnitureSetItemController.Delete(newSetItems);*/

        }
    }
}
