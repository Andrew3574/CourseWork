using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FurnitureDBLibrary.Models.Manufacturers;
using FurnitureDBLibrary.Models.FurnitureTypes;

namespace FurnitureShopUnitTests
{
    [TestClass]
    public class ManufacturerTests
    {
        readonly ManufacturerController manufacturerController = new ManufacturerController();

        Manufacturer newManufacturer = new SpecificManufacturer("Особый", (decimal)0.20);

        [TestMethod]
        public void ReadTest()
        {
            manufacturerController.Create(newManufacturer);
            Manufacturer manufacturer = manufacturerController.Read().Find(manufact => manufact.ManufacturerName == "Особый");
            decimal expectedMarkup = (decimal)0.20;
            Assert.AreEqual(expectedMarkup, manufacturer.ManufacturerMarkup);
            manufacturerController.Delete(newManufacturer);

        }

        [TestMethod]
        public void Createtest()
        {
            manufacturerController.Create(newManufacturer);
            manufacturerController.Delete(newManufacturer);
        }

        [TestMethod]
        public void Updatetest()
        {
            manufacturerController.Create(newManufacturer);
            decimal expectedMarkup = (decimal)1;
            newManufacturer.ManufacturerMarkup = (decimal)1;

            manufacturerController.Update(newManufacturer);
            Manufacturer manufacturer = manufacturerController.Read().Find(manufact => manufact.ManufacturerName == "Особый");
            Assert.AreEqual(expectedMarkup, manufacturer.ManufacturerMarkup);
            manufacturerController.Delete(manufacturer);

        }

        [TestMethod]
        public void Deletetest()
        {
            manufacturerController.Create(newManufacturer);
            manufacturerController.Delete(newManufacturer);
        }


    }
}
