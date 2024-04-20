using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.FurnitureSetItems;
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
            ManufacturerController manufacturerController = new ManufacturerController();
            List<Manufacturer> manufacturers = manufacturerController.Read();
            FurnitureTypeController furnitureTypeController = new FurnitureTypeController();
            List<FurnitureType> furnitureTypes = furnitureTypeController.Read();

            List<FurnitureSetItem> furnitureSetItems = new List<FurnitureSetItem>();
            string command = $"select furniturename,furnitureprice,furniturequantity,furnituretypename,manufacturername,furnituresetname " +
                $"from furnituresetitems " +
                $"join furnitures on furnitures.furnitureid = furnituresetitems.furnitureid " +
                $"join furnituresets on furnituresets.furnituresetid = furnituresetitems.furnituresetid " +
                $"join furnituretypes on furnitures.furnituretypeid = furnituretypes.furnituretypeid " +
                $"join manufacturers on manufacturers.manufacturerid = furnitures.manufacturerid " +
                $"order by @param;";

            NpgsqlParameter idParam = new NpgsqlParameter("@param", "furnituresetid");
            _command.CommandText = command;
            _command.Parameters.Add(idParam);

            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    var type = furnitureTypes.Find(t => t.TypeName == reader.GetString(3));
                    var manufacturer = manufacturers.Find(m => m.ManufacturerName == reader.GetString(4));

                    switch (reader.GetString(5).ToLower())
                    {
                        case "кухонный":
                            furnitureSetItems.Add(new KitchenSetItem(reader.GetString(0),reader.GetDecimal(1),reader.GetInt32(2),type, manufacturer));
                            break;

                        case "спальный":
                            furnitureSetItems.Add(new BedroomSetItem(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), type, manufacturer));
                            break;

                        case "офисный":
                            furnitureSetItems.Add(new OfficeSetItem(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), type, manufacturer));
                            break;

                        case "гостинный":
                            furnitureSetItems.Add(new LoungeSetItem(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), type, manufacturer));
                            break;

                        default:
                            throw new Exception("Магазин не реализует данный тип гарнитура");
                            
                    }
                }                
            }

            reader.Close();
            return furnitureSetItems;
        }

        public override void Create(FurnitureSetItem model)
        {
            int setId, furnitureId;

            string command = $"select GetSetId('{model.FurnitureSetName}')";
            _command.CommandText = command;
            setId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"select GetFurnitureId('{model.FurnitureName}')";
            _command.CommandText = command;
            furnitureId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"insert into furnituresetitems values ({setId},{furnitureId});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Delete(FurnitureSetItem model)
        {
            int setId, furnitureId;

            string command = $"select GetSetId('{model.FurnitureSetName}')";
            _command.CommandText = command;
            setId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"select GetFurnitureId('{model.FurnitureName}')";
            _command.CommandText = command;
            furnitureId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"delete from furnituresetitems where furnituresetitemid = {setId} and where furnitureid = {furnitureId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Update(FurnitureSetItem model)
        {

            int setId, furnitureId;

            string command = $"select GetSetId('{model.FurnitureSetName}')";
            _command.CommandText = command;
            setId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"select GetFurnitureId('{model.FurnitureName}')";
            _command.CommandText = command;
            furnitureId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"update furnituresetitems set furnituresetid = {setId}, furnitureid = {furnitureId} where furnituresetitemid = {setId} and where furnitureid = {furnitureId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();

        }

    }
}
