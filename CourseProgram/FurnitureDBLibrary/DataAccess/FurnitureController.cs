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
                reader.Close();
            }

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
            string command = $"update furnitures set furniturename = {model.FurnitureName},furnitureprice={model.FurniturePrice},furniturequantity={model.FurnitureQuantity},furnituretypeid={model.FurnitureTypeId},manufacturerid={model.FurnitureManufacturerId};";
            _command.CommandText= command;
            _command.ExecuteNonQuery();
        
        }
    }
}
