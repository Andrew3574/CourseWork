using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.Furnitures;
using FurnitureDBLibrary.Models.FurnitureTypes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class FurnitureController : DBObjectController<Furniture>
    {
        public FurnitureController() : base() { }

        public override List<Furniture> Read()
        {
            ManufacturerController manufacturerController = new ManufacturerController();
            List<Manufacturer> manufacturers = manufacturerController.Read();
            FurnitureTypeController furnitureTypeController = new FurnitureTypeController();
            List<FurnitureType> furnitureTypes = furnitureTypeController.Read();

            List<Furniture> furnitures = new List<Furniture>();

            string command = "select furniturename,furnitureprice,furniturequantity,furnituretypename,manufacturername,varietyname from furnitures " +
                "join furnituretypes on furnitures.furnituretypeid = furnituretypes.furnituretypeid " +
                "join manufacturers on manufacturers.manufacturerid = furnitures.manufacturerid " +
                "join furniturevarieties on furniturevarieties.varietyid = furnitures.varietyid order by @param;";

            NpgsqlParameter idParam = new NpgsqlParameter("@param", "furnitureid");
            _command.CommandText = command;
            _command.Parameters.Add(idParam);
            _command.ExecuteNonQuery();

            NpgsqlDataReader reader = _command.ExecuteReader();            

            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    var type = furnitureTypes.Find(t => t.TypeName == reader.GetString(3));
                    var manufacturer = manufacturers.Find(m => m.ManufacturerName == reader.GetString(4));

                    switch (reader.GetString(5).ToLower())
                    {
                        case "стул":

                            switch (reader.GetString(3).ToLower())
                            {
                                case "кухонная":
                                    furnitures.Add(new KitchenChair(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2)));
                                    break;

                                case "офисная":
                                    furnitures.Add(new OfficeChair(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2)));
                                    break;

                            }
                            break;

                        case "стол":

                            switch (reader.GetString(3).ToLower())
                            {
                                case "кухонная":
                                    furnitures.Add(new KitchenTable(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2)));
                                    break;

                                case "офисная":
                                    furnitures.Add(new OfficeTable(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2)));
                                    break;

                            }
                            break;

                        case "шкаф":

                            switch (reader.GetString(3).ToLower())
                            {
                                case "спальная":
                                    furnitures.Add(new BedroomCloset(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2)));
                                    break;

                                case "офисная":
                                    furnitures.Add(new OfficeCloset(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2)));
                                    break;

                            }
                            break;

                        case "диван":

                            switch (reader.GetString(3).ToLower())
                            {
                                case "гостинная":
                                    furnitures.Add(new LoungeSofa(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2)));
                                    break;

                            }
                            break;

                        default:
                            throw new Exception("Магазин не продает данной мебели");
                    }
                }
            }

            reader.Close();
            return furnitures;
        }

        public override void Create(Furniture model)
        {            
            short typeId, manufactId;
            int varietyId;

            string command = $"select GetTypeId('{model.TypeName}')";
            _command.CommandText = command;
            typeId = Convert.ToInt16(_command.ExecuteScalar());

            command = $"select GetManufacturerId('{model.ManufacturerName}')";
            _command.CommandText = command;
            manufactId = Convert.ToInt16(_command.ExecuteScalar());

            command = $"select GetVarietyId('{model.FurnitureVariety.ToLower()}')";
            _command.CommandText = command;
            varietyId = Convert.ToInt32(_command.ExecuteScalar());

            command = $"insert into furnitures(furniturename,furnitureprice,furniturequantity,furnituretypeid,manufacturerid,varietyid) values " +
                $"('{model.FurnitureName}',{model.FurniturePrice.ToString().Replace(',','.')},{model.FurnitureQuantity},{typeId},{manufactId},{varietyId});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        
        }

        public override void Delete(Furniture model)
        {
            string command = $"delete from furnitures where furniturename = '{model.FurnitureName}';";
            _command.CommandText = command;
            _command.ExecuteNonQuery();

        }

        public override void Update(Furniture model)
        {
            short typeId, manufactId;

            string command = $"select GetTypeId('{model.TypeName}')";
            _command.CommandText = command;
            typeId = Convert.ToInt16(_command.ExecuteScalar());

            command = $"select GetManufacturerId('{model.ManufacturerName}')";
            _command.CommandText = command;
            manufactId = Convert.ToInt16(_command.ExecuteScalar());

            command = $"update furnitures set furniturename = '{model.FurnitureName}',furnitureprice={model.FurniturePrice.ToString("0.00",System.Globalization.CultureInfo.InvariantCulture)},furniturequantity={model.FurnitureQuantity},furnituretypeid={typeId},manufacturerid={manufactId} where furnitureName = '{model.FurnitureName}';";
            _command.CommandText= command;
            _command.ExecuteNonQuery();
        
        } 
        
        public List<string> GetVarieties(List<Furniture> furnitures)
        {
            List<string> list = new List<string>();
            foreach (Furniture furniture in furnitures)
            {
                list.Add(furniture.FurnitureVariety.ToString());
            }

            return list;
        }
    }
}
