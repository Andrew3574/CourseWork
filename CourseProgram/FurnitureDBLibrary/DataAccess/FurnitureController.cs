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

            string command = "select furniturename,furnitureprice,furniturequantity,furnituretypename,manufacturername,varietyname,image,furnituretypemarkup,manufacturermarkup from furnitures " +
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
                                    Furniture kitchenChair = new KitchenChair(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2));
                                    kitchenChair.TypeName = reader.GetString(3);
                                    kitchenChair.TypeMarkup = reader.GetDecimal(7);
                                    kitchenChair.ManufacturerName = reader.GetString(4);
                                    kitchenChair.ManufacturerMarkup = reader.GetDecimal(8);
                                    kitchenChair.FurnitureImage = reader.GetString(6);
                                    furnitures.Add(kitchenChair);
                                    break;

                                case "офисная":
                                    Furniture officeChair = new OfficeChair(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2));
                                    officeChair.TypeName = reader.GetString(3);
                                    officeChair.TypeMarkup = reader.GetDecimal(7);
                                    officeChair.ManufacturerName = reader.GetString(4);
                                    officeChair.ManufacturerMarkup = reader.GetDecimal(8);
                                    officeChair.FurnitureImage = reader.GetString(6);
                                    furnitures.Add(officeChair);
                                    break;

                            }
                            break;

                        case "стол":

                            switch (reader.GetString(3).ToLower())
                            {
                                case "кухонная":
                                    Furniture kitchenTable = new KitchenTable(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2));
                                    kitchenTable.TypeName = reader.GetString(3);
                                    kitchenTable.TypeMarkup = reader.GetDecimal(7);
                                    kitchenTable.ManufacturerName = reader.GetString(4);
                                    kitchenTable.ManufacturerMarkup = reader.GetDecimal(8);
                                    kitchenTable.FurnitureImage = reader.GetString(6);
                                    furnitures.Add(kitchenTable);
                                    break;

                                case "офисная":
                                    Furniture officeTable = new OfficeTable(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2));
                                    officeTable.TypeName = reader.GetString(3);
                                    officeTable.TypeMarkup = reader.GetDecimal(7);
                                    officeTable.ManufacturerName = reader.GetString(4);
                                    officeTable.ManufacturerMarkup = reader.GetDecimal(8);
                                    officeTable.FurnitureImage = reader.GetString(6);
                                    furnitures.Add(officeTable);
                                    break;

                            }
                            break;

                        case "шкаф":

                            switch (reader.GetString(3).ToLower())
                            {
                                case "спальная":
                                    Furniture bedroomCloset = new BedroomCloset(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2));
                                    bedroomCloset.TypeName = reader.GetString(3);
                                    bedroomCloset.TypeMarkup = reader.GetDecimal(7);
                                    bedroomCloset.ManufacturerName = reader.GetString(4);
                                    bedroomCloset.ManufacturerMarkup = reader.GetDecimal(8);
                                    bedroomCloset.FurnitureImage = reader.GetString(6);
                                    furnitures.Add(bedroomCloset);
                                    break;

                                case "офисная":
                                    Furniture officeCloset = new OfficeCloset(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2));
                                    officeCloset.TypeName = reader.GetString(3);
                                    officeCloset.TypeMarkup = reader.GetDecimal(7);
                                    officeCloset.ManufacturerName = reader.GetString(4);
                                    officeCloset.ManufacturerMarkup = reader.GetDecimal(8);
                                    officeCloset.FurnitureImage = reader.GetString(6);
                                    furnitures.Add(officeCloset);
                                    break;

                            }
                            break;

                        case "диван":

                            switch (reader.GetString(3).ToLower())
                            {
                                case "гостиная":
                                    Furniture loungeSofa = new LoungeSofa(reader.GetString(0), reader.GetDecimal(1), reader.GetInt32(2));
                                    loungeSofa.TypeName = reader.GetString(3);
                                    loungeSofa.TypeMarkup = reader.GetDecimal(7);
                                    loungeSofa.ManufacturerName = reader.GetString(4);
                                    loungeSofa.ManufacturerMarkup = reader.GetDecimal(8);
                                    loungeSofa.FurnitureImage = reader.GetString(6);
                                    furnitures.Add(loungeSofa);
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
