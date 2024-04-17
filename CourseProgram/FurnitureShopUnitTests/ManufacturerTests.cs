using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FurnitureShopUnitTests
{
    [TestClass]
    public class ManufacturerTests
    {
        readonly ManufacturerController controller = new ManufacturerController();
        readonly Manufacturer manufacturer = new Manufacturer(4, "qwerty", (decimal)0.20);

        [TestMethod]
        public void CreateTest()
        {                        
            controller.Create(manufacturer);
            controller.Delete(manufacturer);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Manufacturer manufacturer = new Manufacturer(4, "qwerty", (decimal)0.35);
            controller.Create(manufacturer);
            controller.Update(manufacturer);
            decimal actualMarkup = controller.Read()[3].ManufacturerMarkup;
            decimal expectedMarkup = (decimal)0.35;
            Assert.AreEqual(expectedMarkup, actualMarkup);
            controller.Delete(manufacturer);

        }

        [TestMethod]
        public void ReadTest()
        {
            short expectedId = 1;
            short actualId = controller.Read()[0].ManufacturerId;
            Assert.AreEqual(expectedId, actualId);

        }

        [TestMethod]
        public void DeleteTest()
        {
            controller.Delete(manufacturer);
        }


    }
}
