using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.Models.Manufacturers;
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
            string command = "select manufacturername,manufacturermarkup from manufacturers;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    switch (reader.GetString(0))
                    {
                        case "ООО «ДЕЛКОМ40»":
                            manufacturers.Add(new Delcom(reader.GetString(0), reader.GetDecimal(1)));
                            break;

                        case "ОАО «Речицадрев»":
                            manufacturers.Add(new Rechicadrev(reader.GetString(0), reader.GetDecimal(1)));
                            break;

                        case "ЗАО «Мозырьлес»":
                            manufacturers.Add(new Mozirles(reader.GetString(0), reader.GetDecimal(1)));
                            break;

                        default:
                            throw new Exception("Магазин не сотрудничает с данным производителем");

                    }

                }                
            }

            reader.Close();
            return manufacturers;
        }

        public override void Create(Manufacturer model)
        {
            string command = $"insert into manufacturers(manufacturername,manufacturermarkup) values ('{model.ManufacturerName}',{model.ManufacturerMarkup.ToString().Replace(',','.')});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }

        public override void Delete(Manufacturer model)
        {
            string command = $"delete from manufacturers where manufacturername = '{model.ManufacturerName}';";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Update(Manufacturer model)
        {
            string command = $"update manufacturers set manufacturermarkup = {model.ManufacturerMarkup.ToString().Replace(',', '.')} where manufacturerName = '{model.ManufacturerName}';";
            _command.CommandText= command;
            _command.ExecuteNonQuery();
        
        }
    }
}
