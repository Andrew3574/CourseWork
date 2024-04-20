using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.CurrentFurnitures;
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
                            furnitures.Add(new Chair(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), type, manufacturer));
                            break;

                        case "стол":
                            furnitures.Add(new Table(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), type, manufacturer));
                                break;

                        case "шкаф":
                            furnitures.Add(new Closet(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), type, manufacturer));
                            break;

                        case "диван":
                            furnitures.Add(new Sofa(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2), type, manufacturer));
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

            string command = $"select GetTypeId('{model.FurnitureType.TypeName}')";
            _command.CommandText = command;
            typeId = Convert.ToInt16(_command.ExecuteScalar());

            command = $"select GetManufacturerId('{model.FurnitureManufacturer.ManufacturerName}')";
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

            string command = $"select GetTypeId('{model.FurnitureType.TypeName}')";
            _command.CommandText = command;
            typeId = Convert.ToInt16(_command.ExecuteScalar());

            command = $"select GetManufacturerId('{model.FurnitureManufacturer.ManufacturerName}')";
            _command.CommandText = command;
            manufactId = Convert.ToInt16(_command.ExecuteScalar());

            command = $"update furnitures set furniturename = '{model.FurnitureName}',furnitureprice={model.FurniturePrice.ToString("0.00",System.Globalization.CultureInfo.InvariantCulture)},furniturequantity={model.FurnitureQuantity},furnituretypeid={typeId},manufacturerid={manufactId} where furnitureName = '{model.FurnitureName}';";
            _command.CommandText= command;
            _command.ExecuteNonQuery();
        
        }       
    }
}
