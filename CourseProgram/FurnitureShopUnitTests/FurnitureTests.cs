using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FurnitureDBLibrary;
using System;
using Npgsql;
using FurnitureDBLibrary.Models.CurrentFurnitures;

namespace FurnitureShopUnitTests
{
    [TestClass]
    public class FurnitureTests
    {
        readonly FurnitureController furnitureController = new FurnitureController();

        [TestMethod]
        public void FurnitureReadTest()
        {
            /*Table table = new Table("Обеденный стол", 2100, 46, "Кухонная", "ООО «ДЕЛКОМ40»");*/
            Assert.AreEqual(furnitureController.Read()[0].FurnitureQuantity, 46);
        }

        /*[TestMethod]
        public void FurnitureCreateTests()
        {
            Furniture newFurniture = new Furniture(11, "Кровать", 2100, 34, 3, 2);
            furnitureController.Create(newFurniture);
            furnitureController.Delete(newFurniture);
        }

        [TestMethod]
        public void FurnitureUpdateTest()
        {
            Furniture newFurniture = new Furniture(1, "Обеденный стол", 2100, 10, 2, 1)
            {
                FurnitureQuantity = 999
            };
            furnitureController.Update(newFurniture);
        }

        [TestMethod]
        public void FurnitureDeleteTest()
        {
            Furniture newFurniture = new Furniture(11, "Кровать", 2100, 34, 3, 2);
            furnitureController.Create(newFurniture);
            newFurniture = new Furniture(11, "Кровать", 2100, 999, 3, 2);
            furnitureController.Delete(newFurniture);
        }*/


    }
}
