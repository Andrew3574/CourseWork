using FurnitureDBLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return sale;
        }
    }
}
