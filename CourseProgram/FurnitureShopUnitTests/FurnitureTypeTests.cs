using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.FurnitureTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql.Internal;
using System;

namespace FurnitureShopUnitTests
{
    [TestClass]
    public class FurnitureTypeTests
    {
        readonly FurnitureTypeController furnitureTypeController = new FurnitureTypeController();

        FurnitureType newFurnitureType = new Specific("Особая", (decimal)0.99);

        [TestMethod]
        public void ReadTest()
        {
            furnitureTypeController.Create(newFurnitureType);
            FurnitureType furnitureType = furnitureTypeController.Read().Find(type => type.TypeName == "Особая");
            decimal expectedMarkup = (decimal)0.99;
            Assert.AreEqual(expectedMarkup, furnitureType.TypeMarkup);
            furnitureTypeController.Delete(newFurnitureType);
            
        }

        [TestMethod]
        public void Createtest()
        {
            furnitureTypeController.Create(newFurnitureType);
            furnitureTypeController.Delete(newFurnitureType);
        }

        [TestMethod]
        public void Updatetest()
        {
            furnitureTypeController.Create(newFurnitureType);
            decimal expectedMarkup = (decimal)1;
            newFurnitureType.TypeMarkup = (decimal)1;

            furnitureTypeController.Update(newFurnitureType);
            FurnitureType furnitureType = furnitureTypeController.Read().Find(type => type.TypeName == "Особая");
            Assert.AreEqual(expectedMarkup, furnitureType.TypeMarkup);
            furnitureTypeController.Delete(furnitureType);
                
        }

        [TestMethod]
        public void Deletetest()
        {
            furnitureTypeController.Create(newFurnitureType);
            furnitureTypeController.Delete(newFurnitureType);
        }


    }
}
