using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.FurnitureTypes;
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
            ManufacturerController manufacturerController = new ManufacturerController();
            List<Manufacturer> manufacturers = manufacturerController.Read();
            FurnitureTypeController furnitureTypeController = new FurnitureTypeController();
            List<FurnitureType> furnitureTypes = furnitureTypeController.Read();

            List<Sale> sales = new List<Sale>();
            string command = "select furniturename,furnitureprice,furnituretypename,manufacturername,furnituresaledquantity,saledate " +
                "from sales join furnitures on furnitures.furnitureid = sales.furnitureid join furnituretypes on furnitures.furnituretypeid = furnituretypes.furnituretypeid join manufacturers on manufacturers.manufacturerid = furnitures.manufacturerid";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var type = furnitureTypes.Find(t => t.TypeName == reader.GetString(2));
                    var manufacturer = manufacturers.Find(m => m.ManufacturerName == reader.GetString(3));

                    sales.Add(new Sale(reader.GetString(0), reader.GetDecimal(1), manufacturer.ManufacturerName, type.TypeName, reader.GetInt32(4), reader.GetDateTime(5)));
                }
                
            }

            reader.Close();
            return sales;
        }

        public override void Create(Sale model)
        {
            int furnitureId;

            string command = $"select GetFurnitureId('{model.FurnitureName}')";
            _command.CommandText = command;
            furnitureId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"insert into sales(furnitureid,furnituresaledquantity,saledate) values ({furnitureId},{model.FurnitureSaledQuantity},'{model.SaleDate}');";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }

        public override void Update(Sale model)
        {
            int furnitureId;

            string command = $"select GetFurnitureId('{model.FurnitureName}')";
            _command.CommandText = command;
            furnitureId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"update sales set furnitureid = {furnitureId}, furnituresaledquantity = {model.FurnitureSaledQuantity}, saledate = '{model.SaleDate}' where furnitureid = '{furnitureId}' and saledate = '{model.SaleDate}' ;";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }

        public override void Delete(Sale model)
        {
            throw new Exception("Нельзя удалять отчеты!");
        }

        public void GenerateXMLReport(List<Sale> sales)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("D:\\КурсоваяРабота\\CourseProgram\\FurnitureDBLibrary\\FurnitureShopReport.xml");
            XmlElement xRoot = xmlDocument.DocumentElement;

            if (xRoot != null)
                xRoot.RemoveAll();

            foreach (Sale s in sales)
            {
                
                XmlElement sale = xmlDocument.CreateElement("sale");

                XmlElement furniture = xmlDocument.CreateElement("furniture");
                XmlElement retailPrice = xmlDocument.CreateElement("retailPrice");
                XmlElement manufacturer = xmlDocument.CreateElement("manufacturer");
                XmlElement type = xmlDocument.CreateElement("type");
                XmlElement quantity = xmlDocument.CreateElement("quantity");
                XmlElement totalCost = xmlDocument.CreateElement("totalCost");
                XmlElement date = xmlDocument.CreateElement("date");

                XmlText furnitureText = xmlDocument.CreateTextNode($"{s.FurnitureName}");
                XmlText retailPriceText = xmlDocument.CreateTextNode($"{s.FurnitureRetailPrice}");
                XmlText manufacturerText = xmlDocument.CreateTextNode($"{s.ManufacturerName}");
                XmlText typeText = xmlDocument.CreateTextNode($"{s.TypeName}");
                XmlText quantityText = xmlDocument.CreateTextNode($"{s.FurnitureSaledQuantity}");
                XmlText totalCostText = xmlDocument.CreateTextNode($"{s.TotalCost}");
                XmlText dateText = xmlDocument.CreateTextNode($"{s.SaleDate:d}");

                furniture.AppendChild(furnitureText);
                retailPrice.AppendChild(retailPriceText);
                manufacturer.AppendChild(manufacturerText);
                type.AppendChild(typeText);
                quantity.AppendChild(quantityText);
                totalCost.AppendChild(totalCostText);
                date.AppendChild(dateText);


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
