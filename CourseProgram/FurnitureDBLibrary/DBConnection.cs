using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;

namespace FurnitureDBLibrary
{
    public class DBConnection
    {
        private static readonly DBConnection _instance = new DBConnection();

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["NpgsqlConnectionString"].ConnectionString;

        private readonly NpgsqlConnection _connection;

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
