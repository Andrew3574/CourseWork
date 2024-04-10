using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public abstract class DBObjectController<T>
    {
        private readonly NpgsqlConnection _connection;
        protected NpgsqlCommand _command;

        public DBObjectController()
        {
            _connection = DBConnection.GetInstance.Connection;
            _command = new NpgsqlCommand();
            _command.Connection = _connection;
            _command.CommandType = System.Data.CommandType.Text;

        }

        public abstract List<T> Read();
        public abstract void Create(T model);
        public abstract void Delete(T model);
        public abstract void Update(T model);

    }
}
