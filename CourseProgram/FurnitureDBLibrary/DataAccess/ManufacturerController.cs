using FurnitureDBLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class ManufacturerController : DBObjectController<Manufacturer>
    {
        public ManufacturerController() : base() { }

        public override List<Manufacturer> Read()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            string command = "select * from manufacturers;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    manufacturers.Add(new Manufacturer(reader.GetInt16(0),reader.GetString(1),reader.GetDecimal(2)));
                }                
            }

            reader.Close();
            return manufacturers;
        }

        public override void Create(Manufacturer model)
        {
            string command = $"insert into manufacturers(manufacturerid,manufacturername,manufacturermarkup) values ({model.ManufacturerId},'{model.ManufacturerName}',{model.ManufacturerMarkup.ToString().Replace(',','.')});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }

        public override void Delete(Manufacturer model)
        {
            string command = $"delete from manufacturers where manufacturerId = {model.ManufacturerId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Update(Manufacturer model)
        {
            string command = $"update manufacturers set manufacturerName = '{model.ManufacturerName}', manufacturermarkup = {model.ManufacturerMarkup.ToString().Replace(',', '.')} where manufacturerid = {model.ManufacturerId};";
            _command.CommandText= command;
            _command.ExecuteNonQuery();
        
        }
    }
}
