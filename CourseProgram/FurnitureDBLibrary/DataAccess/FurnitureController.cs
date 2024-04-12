using FurnitureDBLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class FurnitureController :DBObjectController<Furniture>
    {
        public FurnitureController() : base() { }

        public override List<Furniture> Read()
        {
            List<Furniture> furnitures = new List<Furniture>();
            string command = "select * from furnitures ;";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    furnitures.Add(new Furniture(reader.GetInt32(0),reader.GetString(1),reader.GetDecimal(2),reader.GetInt32(3),reader.GetInt16(4),reader.GetInt16(5)));
                }
            }

            reader.Close();
            return furnitures;
        }

        public override void Create(Furniture model)
        {
            string command = $"insert into furnitures values ({model.FurnitureName},{model.FurniturePrice},{model.FurnitureQuantity},{model.FurnitureTypeId},{model.FurnitureManufacturerId});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Delete(Furniture model)
        {
            string command = $"delete from furnitures where furnitureid = {model.FurnitureId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();

        }

        public override void Update(Furniture model)
        {
            string command = $"update furnitures set furniturename = '{model.FurnitureName}',furnitureprice={model.FurniturePrice.ToString("0.00",System.Globalization.CultureInfo.InvariantCulture)},furniturequantity={model.FurnitureQuantity},furnituretypeid={model.FurnitureTypeId},manufacturerid={model.FurnitureManufacturerId} where furnitureName = '{model.FurnitureName}';";
            _command.CommandText= command;
            _command.ExecuteNonQuery();
        
        }

        public string[] GetCurrentFurnitureInfo(Furniture currentFurniture,List<Furniture> furnitures,List<Manufacturer> manufacturers, List<FurnitureType> furnitureTypes)
        {
            string[] furn = new string[5];
            var info = from furniture in furnitures
                       where furniture.FurnitureName == currentFurniture.FurnitureName
                       join manufact in manufacturers on furniture.FurnitureManufacturerId equals manufact.ManufacturerId
                       join type in furnitureTypes on furniture.FurnitureTypeId equals type.TypeId
                       select new { furniture.FurnitureName, RetailPrice = (furniture.FurniturePrice + furniture.FurniturePrice * type.TypeMarkup + furniture.FurniturePrice * manufact.ManufacturerMarkup),furniture.FurnitureQuantity, type.TypeName, manufact.ManufacturerName };

            var curFurniture = info.First();
            furn = $"{curFurniture.FurnitureName}/{curFurniture.RetailPrice:#.00}/{curFurniture.ManufacturerName}/{curFurniture.TypeName}/{curFurniture.FurnitureQuantity}".Split('/');

            return furn;
        }
    }
}
