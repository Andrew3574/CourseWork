using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.Models.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
             FurnitureController controller = new FurnitureController();
             FurnitureTypeController furnitureTypeController = new FurnitureTypeController();
             ManufacturerController manufacturerController = new ManufacturerController();

             List<Manufacturer> manufacturers = manufacturerController.Read();
             List<FurnitureType> furnitureTypes = furnitureTypeController.Read();
             List<Furniture> furnitures = new List<Furniture>();

            /* FurnitureType type = furnitureTypes.Find(t => t.TypeName == "Кухонная");
             Manufacturer manufacturer = manufacturers.Find(m => m.ManufacturerName == "ОАО «Речицадрев»");

             furnitures = controller.Read();

             foreach (Furniture f in furnitures)
             {
                 Console.WriteLine($"{f.FurnitureName} | {f.FurniturePrice} | {f.FurnitureQuantity} | {f.FurnitureType.TypeName} | {f.FurnitureManufacturer.ManufacturerName} | {f.FurnitureVariety}\n");
             }

             Table table = new Table("Новый стол",1899,45, type, manufacturer);

             controller.Create(table);

             furnitures = controller.Read();
             Console.WriteLine("--------------------------------------------------");
             foreach (Furniture f in furnitures)
             {
                 Console.WriteLine($"{f.FurnitureName} | {f.FurniturePrice} | {f.FurnitureQuantity} | {f.FurnitureType.TypeName} | {f.FurnitureManufacturer.ManufacturerName} | {f.FurnitureVariety}\n");
             }

             table.FurnitureQuantity = 999999;
             controller.Update(table);
             furnitures = controller.Read();
             Console.WriteLine("--------------------------------------------------");
             foreach (Furniture f in furnitures)
             {
                 Console.WriteLine($"{f.FurnitureName} | {f.FurniturePrice} | {f.FurnitureQuantity} | {f.FurnitureType.TypeName} | {f.FurnitureManufacturer.ManufacturerName} | {f.FurnitureVariety}\n");
             }

             controller.Delete(table);*/



            /*Console.WriteLine("---FURNITURETYPES--------------------------------------------------");

            FurnitureType furnitureType = new KitchenType("Кухонная", (decimal)0.24);

            furnitureTypes = furnitureTypeController.Read();

            foreach (FurnitureType t in furnitureTypes)
            {
                Console.WriteLine($"{t.TypeName} | {t.TypeMarkup}\n");
            }

            furnitureTypes = furnitureTypeController.Read();

            foreach (FurnitureType t in furnitureTypes)
            {
                Console.WriteLine($"{t.TypeName} | {t.TypeMarkup}\n");
            }

            furnitureType.TypeMarkup = (decimal)0.15;
            furnitureTypeController.Update(furnitureType);

            furnitureTypes = furnitureTypeController.Read();

            foreach (FurnitureType t in furnitureTypes)
            {
                Console.WriteLine($"{t.TypeName} | {t.TypeMarkup}\n");
            }*/

            FurnitureType furnitureType = new LoungeType("Гостинная", (decimal)0.24);
            Manufacturer manufacturer = new Mozirles("ЗАО «Мозырьлес»", (decimal)0.99);

            Sale sale = new Sale("Шкаф",1999, furnitureType, manufacturer,1,DateTime.Today);
            SaleController saleController = new SaleController();
            sale.FurnitureSaledQuantity = 999;
            saleController.Update(sale);
            List<Sale> sales = saleController.Read();

            foreach(Sale s in sales)
            {
                Console.WriteLine($"{s.FurnitureName} | {s.FurniturePrice:#.00} | {s.FurnitureManufacturer.ManufacturerName} | {s.FurnitureType.TypeName} | {s.FurnitureSaledQuantity} | {s.GetTotalCost()} | {s.SaleDate:d}\n");
            }
            Console.ReadLine();
        }
    }
}
