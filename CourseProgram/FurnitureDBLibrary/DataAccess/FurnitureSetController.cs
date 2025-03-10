﻿using FurnitureDBLibrary.Models;
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
            string command = "select furnituresetname from furnituresets;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    furnitureSets.Add(new FurnitureSet(reader.GetString(0)));
                }
            }

            reader.Close();
            return furnitureSets;
        }

        public override void Create(FurnitureSet model)
        {
            string command = $"insert into furnituresets values ({model.FurnitureSetName});";
            _command.CommandText = command;
            _command.ExecuteNonQuery();

        }

        public override void Delete(FurnitureSet model)
        {
            string command = $"delete from furnituresets where furnituresetid = {model.FurnitureSetName}";
            _command.CommandText = command;
            _command.ExecuteNonQuery();

        }

        public override void Update(FurnitureSet model)
        {
            string command = $"update furnituresets set furnituresetname = '{model.FurnitureSetName}' where furnituresetname = {model.FurnitureSetName};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();

        }

        

    }
}
