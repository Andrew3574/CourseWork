using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FurnitureDBLibrary;
using System;
using Npgsql;
using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.Furnitures;
using System.Collections.Generic;

namespace FurnitureShopUnitTests
{
    [TestClass]
    public class FurnitureTests
    {
        readonly FurnitureController furnitureController = new FurnitureController();

        Furniture newFurniture = new KitchenTable("Тестовый стол", 2100, 22);

        [TestMethod]
        public void FurnitureCreateTests()
        {
            if (furnitureController.Read().Find(f => f.FurnitureName == "Тестовый стол") != null)
                furnitureController.Delete(newFurniture);

            furnitureController.Create(newFurniture);
        }

        [TestMethod]
        public void FurnitureUpdateTest()
        {
            if (furnitureController.Read().Find(f => f.FurnitureName == "Тестовый стол") != null)
                furnitureController.Delete(newFurniture);

            furnitureController.Create(newFurniture);
            int expected = 33;
            newFurniture.FurnitureQuantity = 33;
            furnitureController.Update(newFurniture);
            Assert.AreEqual(expected, furnitureController.Read().Find(type => type.FurnitureName == "Тестовый стол").FurnitureQuantity);
            furnitureController.Delete(newFurniture);
        }

        [TestMethod]
        public void FurnitureReadTest()
        {
            if (furnitureController.Read().Find(f => f.FurnitureName == "Тестовый стол") != null)
                furnitureController.Delete(newFurniture);

            furnitureController.Create(newFurniture);
            int expected = 22;
            Furniture furniture = furnitureController.Read().Find(f => f.FurnitureName == "Тестовый стол");
            Assert.AreEqual(expected, furniture.FurnitureQuantity);
            furnitureController.Delete(newFurniture);
        }


        [TestMethod]
        public void FurnitureDeleteTest()
        {

            if (furnitureController.Read().Find(f => f.FurnitureName == "Тестовый стол") == null)
                furnitureController.Create(newFurniture);

            furnitureController.Delete(newFurniture);
        }

    }
}
