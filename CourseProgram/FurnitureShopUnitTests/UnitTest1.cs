using FurnitureDBLibrary.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FurnitureShopUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        readonly FurnitureController furnitureController = new FurnitureController();

        [TestMethod]
        public void FurnitureReadTest()
        {
            /*Table table = new Table("Обеденный стол", 2100, 46, "Кухонная", "ООО «ДЕЛКОМ40»");*/
            Assert.AreEqual(furnitureController.Read()[0].FurnitureQuantity, 46);
        }
    }
}
