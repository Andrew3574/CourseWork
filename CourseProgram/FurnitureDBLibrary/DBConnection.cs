using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace FurnitureDBLibrary
{
    public class DBConnection
    {
        private static readonly DBConnection _instance = new DBConnection();

        private string _connectionString = "Server=localhost;Port=5432;Database=FurnitureDB;User Id=postgres;Password=qy5k--zhr8a98L;";

        private NpgsqlConnection _connection;

        private DBConnection()
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }

        public static DBConnection GetInstance { get { return _instance; } }

        public NpgsqlConnection Connection { get { return _connection; } }

        public void CloseConnection()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
