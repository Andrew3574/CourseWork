using FurnitureDBLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FurnitureDBLibrary.DataAccess
{
    public class SaleController : DBObjectController<Sale>
    {
        public SaleController() : base() { }
        public override List<Sale> Read()
        {
            List<Sale> sales = new List<Sale>();
            string command = "select * from sales";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sales.Add(new Sale(reader.GetInt32(0),reader.GetInt32(1),reader.GetInt32(2),reader.GetDateTime(3)));
                }
                reader.Close();
            }

            return sales;
        }

        public override void Create(Sale model)
        {
            string command = $"insert into sales(saleid,furnitureid,furnituresaledquantity,saledate) values ({model.SaleId},{model.FurnitureId},{model.FurnitureSaledQuantity},'{model.SaleDate}');";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }

        public override void Update(Sale model)
        {
           string command = $"update sales set furnitureid = {model.FurnitureId}, furnituresaledquantity = {model.FurnitureSaledQuantity}, saledate = '{model.SaleDate}' where furnitureid = '{model.FurnitureId}' and saledate = '{model.SaleDate}' ;"; 
           _command.CommandText = command;
           _command.ExecuteNonQuery();
        }

        public override void Delete(Sale model)
        {
            throw new Exception("Нельзя удалять отчеты!");
        }

        public string[] GetInfo(List<Sale> sales,List<Furniture> furnitures,List<Manufacturer> manufacturers,List<FurnitureType> furnitureTypes)
        {
            string[] sale = new string[sales.Count];
            int i = 0;
            var saleInfo = from s in sales
                           join furniture in furnitures on s.FurnitureId equals furniture.FurnitureId
                           join manufact in manufacturers on furniture.FurnitureManufacturerId equals manufact.ManufacturerId
                           join type in furnitureTypes on furniture.FurnitureTypeId equals type.TypeId
                           select new 
                           { 
                               s.SaleId,
                               furniture.FurnitureName, 
                               RetailPrice = (furniture.FurniturePrice + furniture.FurniturePrice * type.TypeMarkup + furniture.FurniturePrice * manufact.ManufacturerMarkup),
                               manufact.ManufacturerName,
                               type.TypeName,
                               s.FurnitureSaledQuantity,
                               s.SaleDate
                           };
            foreach (var s in saleInfo)
            {
                sale[i] += $"{s.SaleId}/{s.FurnitureName}/{s.RetailPrice:#.00}/{s.ManufacturerName}/{s.TypeName}/{s.FurnitureSaledQuantity}/{s.RetailPrice * s.FurnitureSaledQuantity}/{s.SaleDate:d}";
                i++;
            }

            return sale;
        }

        public string[] GetInfoByManufacturer(string manufacturer, List<Sale> sales, List<Furniture> furnitures, List<Manufacturer> manufacturers, List<FurnitureType> furnitureTypes)
        {
            string[] sale = new string[sales.Count];
            int i = 0;
            var saleInfo = from s in sales
                           join furniture in furnitures on s.FurnitureId equals furniture.FurnitureId
                           join manufact in manufacturers on furniture.FurnitureManufacturerId equals manufact.ManufacturerId
                           join type in furnitureTypes on furniture.FurnitureTypeId equals type.TypeId
                           where manufact.ManufacturerName == manufacturer
                           select new
                           {
                               s.SaleId,
                               furniture.FurnitureName,
                               RetailPrice = (furniture.FurniturePrice + furniture.FurniturePrice * type.TypeMarkup + furniture.FurniturePrice * manufact.ManufacturerMarkup),
                               manufact.ManufacturerName,
                               type.TypeName,
                               s.FurnitureSaledQuantity,
                               s.SaleDate
                           };
            foreach (var s in saleInfo)
            {
                sale[i] += $"{s.SaleId}/{s.FurnitureName}/{s.RetailPrice:#.00}/{s.ManufacturerName}/{s.TypeName}/{s.FurnitureSaledQuantity}/{s.RetailPrice * s.FurnitureSaledQuantity}/{s.SaleDate:d}";
                i++;
            }

            return sale;
        }

        public void GenerateXMLReport(string[] info)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("D:\\КурсоваяРабота\\CourseProgram\\FurnitureDBLibrary\\FurnitureShopReport.xml");
            XmlElement xRoot = xmlDocument.DocumentElement;

            if (xRoot != null)
                xRoot.RemoveAll();

            foreach (string inf in info)
            {
                string[] currentSale = inf.Split('/');

                XmlElement sale = xmlDocument.CreateElement("sale");
                XmlAttribute idAttribute = xmlDocument.CreateAttribute("id");

                XmlElement furniture = xmlDocument.CreateElement("furniture");
                XmlElement retailPrice = xmlDocument.CreateElement("retailPrice");
                XmlElement manufacturer = xmlDocument.CreateElement("manufacturer");
                XmlElement type = xmlDocument.CreateElement("type");
                XmlElement quantity = xmlDocument.CreateElement("quantity");
                XmlElement totalCost = xmlDocument.CreateElement("totalCost");
                XmlElement date = xmlDocument.CreateElement("date");

                XmlText idText = xmlDocument.CreateTextNode($"{currentSale[0]}");
                XmlText furnitureText = xmlDocument.CreateTextNode($"{currentSale[1]}");
                XmlText retailPriceText = xmlDocument.CreateTextNode($"{currentSale[2]}");
                XmlText manufacturerText = xmlDocument.CreateTextNode($"{currentSale[3]}");
                XmlText typeText = xmlDocument.CreateTextNode($"{currentSale[4]}");
                XmlText quantityText = xmlDocument.CreateTextNode($"{currentSale[5]}");
                XmlText totalCostText = xmlDocument.CreateTextNode($"{currentSale[6]}");
                XmlText dateText = xmlDocument.CreateTextNode($"{currentSale[7]}");

                idAttribute.AppendChild(idText);
                furniture.AppendChild(furnitureText);
                retailPrice.AppendChild(retailPriceText);
                manufacturer.AppendChild(manufacturerText);
                type.AppendChild(typeText);
                quantity.AppendChild(quantityText);
                totalCost.AppendChild(totalCostText);
                date.AppendChild(dateText);

                sale.Attributes.Append(idAttribute);

                sale.AppendChild(furniture);
                sale.AppendChild(retailPrice);
                sale.AppendChild(manufacturer);
                sale.AppendChild(type);
                sale.AppendChild(quantity);
                sale.AppendChild(totalCost);
                sale.AppendChild(date);

                xRoot.AppendChild(sale);

                xmlDocument.Save("D:\\КурсоваяРабота\\CourseProgram\\FurnitureDBLibrary\\FurnitureShopReport.xml");

            }
        }
    }
}
