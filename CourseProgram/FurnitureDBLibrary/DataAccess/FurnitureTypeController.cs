using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.FurnitureTypes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class FurnitureTypeController : DBObjectController<FurnitureType>
    {
        public FurnitureTypeController() : base() { }

        public override List<FurnitureType> Read()
        {
            List<FurnitureType> furnitureTypes = new List<FurnitureType>();
            string command = "select furnituretypename,furnituretypemarkup from furnituretypes;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows) 
            {
                while(reader.Read())
                {
                    switch (reader.GetString(0).ToLower())
                    {
                        case "кухонная":
                            furnitureTypes.Add(new KitchenType(reader.GetString(0), reader.GetDecimal(1)));
                            break;

                        case "офисная":
                            furnitureTypes.Add(new OfficeType(reader.GetString(0), reader.GetDecimal(1)));
                            break;

                        case "спальная":
                            furnitureTypes.Add(new BedroomType(reader.GetString(0), reader.GetDecimal(1)));
                            break;

                        case "гостинная":
                            furnitureTypes.Add(new LoungeType(reader.GetString(0), reader.GetDecimal(1)));
                            break;

                        default:
                            throw new Exception("Магазин не реализует данный тип мебели");

                    }
                }                
            }

            reader.Close();
            return furnitureTypes;
        }

        public override void Create(FurnitureType model)
        {
            string command = $"insert into furnituretypes(furnituretypename,furnituretypemarkup) values ('{model.TypeName}',{model.TypeMarkup.ToString().Replace(',','.')});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Delete(FurnitureType model)
        {
            string command = $"delete from furnituretypes where furnituretypename = '{model.TypeName}';";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Update(FurnitureType model)
        {
            string command = $"update furnituretypes set furnituretypemarkup={model.TypeMarkup.ToString().Replace(',','.')} where furnituretypename = '{model.TypeName}';";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }
    }
}
