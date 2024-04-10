using FurnitureDBLibrary.Models;
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
            string command = "select * from furnituretypes;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows) 
            {
                while(reader.Read())
                {
                    furnitureTypes.Add(new FurnitureType(reader.GetInt16(0),reader.GetString(1),reader.GetDecimal(2)));
                }
                reader.Close();
            }

            return furnitureTypes;
        }

        public override void Create(FurnitureType model)
        {
            string command = $"insert into furnituretypes values ({model.TypeName},{model.TypeMarkup});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Delete(FurnitureType model)
        {
            string command = $"delete from furnituretypes where furnituretypeId = {model.TypeId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Update(FurnitureType model)
        {
            string command = $"update furnituretypes set furnituretypeName='{model.TypeName}', furnituretypemarkup={model.TypeMarkup} where furnituretypeId = {model.TypeId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }
    }
}
