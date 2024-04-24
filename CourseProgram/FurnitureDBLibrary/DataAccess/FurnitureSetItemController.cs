using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.Furnitures;
using FurnitureDBLibrary.Models.FurnitureSetItems;
using FurnitureDBLibrary.Models.FurnitureTypes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class FurnitureSetItemController : DBObjectController<SetItems>
    {
        public FurnitureSetItemController() : base() { }

        public override List<SetItems> Read()
        {
            FurnitureController furnitureController = new FurnitureController();
            List<Furniture> furnitures = furnitureController.Read();
            ManufacturerController manufacturerController = new ManufacturerController();
            List<Manufacturer> manufacturers = manufacturerController.Read();
            FurnitureTypeController furnitureTypeController = new FurnitureTypeController();
            List<FurnitureType> furnitureTypes = furnitureTypeController.Read();

            List<SetItems> furnitureSetItems = new List<SetItems>();
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
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    List<Furniture> furnitureList = new List<Furniture>();
                    var type = furnitureTypes.Find(t => t.TypeName == reader.GetString(3));
                    var manufacturer = manufacturers.Find(m => m.ManufacturerName == reader.GetString(4));

                    switch (reader.GetString(5).ToLower())
                    {
                        case "кухонный":                            
                            furnitureList = furnitures.FindAll(f => f.TypeName == "Кухонная");
                            furnitureSetItems.Add(new KitchenSetItems(furnitureList));
                        break;                    

                        case "спальный":
                            furnitureList = furnitures.FindAll(f => f.TypeName == "Спальная");
                            furnitureSetItems.Add(new BedroomSetItems(furnitureList));
                            break;

                        case "офисный":
                            furnitureList = furnitures.FindAll(f => f.TypeName == "Офисная");
                            furnitureSetItems.Add(new OfficeSetItems(furnitureList));
                            break;

                        case "гостинный":
                            furnitureList = furnitures.FindAll(f => f.TypeName == "Гостинная");
                            furnitureSetItems.Add(new LoungeSetItems(furnitureList));
                            break;

                        default:
                            throw new Exception("Магазин не реализует данный тип гарнитура");

                    }
                }
            }

            reader.Close();
            return furnitureSetItems;
        }

        public override void Create(SetItems model)
        {
            int setId, furnitureId;

            try
            {
                string command = $"select GetSetId('{model.SetName}')";
                _command.CommandText = command;
                setId = Convert.ToInt32(_command.ExecuteScalar());

                foreach (var item in model.FurnitureList)
                {
                    command = $"select GetFurnitureId('{item.FurnitureName}')";
                    _command.CommandText = command;
                    furnitureId = Convert.ToInt32(_command.ExecuteScalar());

                    command = $"insert into furnituresetitems values ({setId},{furnitureId});";
                    _command.CommandText = command;
                    _command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }            
        
        }

        public override void Delete(SetItems model)
        {
            int setId, furnitureId;

            try
            {
                string command = $"select GetSetId('{model.SetName}')";
                _command.CommandText = command;
                setId = Convert.ToInt32(_command.ExecuteScalar());

                foreach (var item in model.FurnitureList)
                {
                    command = $"select GetFurnitureId('{item.FurnitureName}')";
                    _command.CommandText = command;
                    furnitureId = Convert.ToInt32(_command.ExecuteScalar());

                    command = $"delete from furnituresetitems where furnituresetitemid = {setId} and where furnitureid = {furnitureId};";
                    _command.CommandText = command;
                    _command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        
        }

        public override void Update(SetItems model)
        {

            int setId, furnitureId;

            try
            {
                string command = $"select GetSetId('{model.SetName}')";
                _command.CommandText = command;
                setId = Convert.ToInt32(_command.ExecuteScalar());

                foreach (var item in model.FurnitureList)
                {
                    command = $"select GetFurnitureId('{item.FurnitureName}')";
                    _command.CommandText = command;
                    furnitureId = Convert.ToInt32(_command.ExecuteScalar());

                    command = $"update furnituresetitems set furnituresetid = {setId}, furnitureid = {furnitureId} where furnituresetitemid = {setId} and where furnitureid = {furnitureId};";
                    _command.CommandText = command;
                    _command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            

        }

    }
}
