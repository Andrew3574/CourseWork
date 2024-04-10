using FurnitureDBLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class FurnitureSetItemController : DBObjectController<FurnitureSetItem>
    {
        public FurnitureSetItemController() : base() { }

        public override List<FurnitureSetItem> Read()
        {
            List<FurnitureSetItem> furnitureSetItems = new List<FurnitureSetItem>();
            string command = $"select * from furnituresetitems;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    furnitureSetItems.Add(new FurnitureSetItem(reader.GetInt32(0),reader.GetInt32(1),reader.GetInt32(2)));
                }
                reader.Close();
            }

            return furnitureSetItems;
        }

        public override void Create(FurnitureSetItem model)
        {
            string command = $"insert into furnituresetitems values ({model.SetItemId},{model.FurnitureSetId},{model.FurnitureId});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Delete(FurnitureSetItem model)
        {
            string command = $"delete from furnituresetitems where furnituresetitemId = {model.SetItemId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Update(FurnitureSetItem model)
        {
            string command = $"update furnituresetitems set furnituresetitemid = {model.SetItemId}, furnituresetid = {model.FurnitureSetId}, furnitureid = {model.FurnitureId} where furnituresetitemId = {model.SetItemId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();

        }

    }
}
