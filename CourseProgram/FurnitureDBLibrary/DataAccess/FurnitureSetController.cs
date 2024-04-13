using FurnitureDBLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class FurnitureSetController : DBObjectController<FurnitureSet>
    {
        public FurnitureSetController() : base() { }

        public override List<FurnitureSet> Read()
        {
            List<FurnitureSet> furnitureSets = new List<FurnitureSet>();
            string command = "select * from furnituresets;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    furnitureSets.Add(new FurnitureSet(reader.GetInt32(0),reader.GetString(1)));
                }                
            }

            reader.Close();
            return furnitureSets;       
        }

        public override void Create(FurnitureSet model)
        {
            string command = $"insert into furnituresets values ({model.FurnitureSetId});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Delete(FurnitureSet model)
        {
            string command = $"delete from furnituresets where furnituresetid = {model.FurnitureSetId}";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Update(FurnitureSet model)
        {
            string command = $"update furnituresets set furnituresetname = '{model.FurnitureSetName}' where furnituresetid = {model.FurnitureSetId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public string GetInfo(FurnitureSet furnitureSet, List<FurnitureSetItem> furnitureSetItems, List<Furniture> furnitures)
        {
            string item = "";
            decimal sum = 0; 
            var info = from furnitureSetItem in furnitureSetItems
                       where furnitureSet.FurnitureSetId == furnitureSetItem.FurnitureSetId
                       join furniture in furnitures on furnitureSetItem.FurnitureId equals furniture.FurnitureId
                       orderby furniture.FurniturePrice
                       select new {furnitureSet.FurnitureSetName,furniture.FurnitureName, furniture.FurniturePrice};

            foreach(var text in info)
            {
                sum += text.FurniturePrice;
                item += $"{text.FurnitureSetName} {text.FurnitureName} {text.FurniturePrice:#.00}\n";
            }

            return item + $"Сумма к оплате: {sum}\n";
        }

    }
}
