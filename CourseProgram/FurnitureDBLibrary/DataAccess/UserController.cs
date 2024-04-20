using FurnitureDBLibrary.UserModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.DataAccess
{
    public class UserController : DBObjectController<User>
    {
        public UserController() : base() { }

        public override List<User> Read()
        {
            _command.Parameters.Clear();
            List<User> users = new List<User>();
            string command = "select username,password,rolename from users join roles on roles.roleid = users.roleid order by @param;";
            NpgsqlParameter roleParam = new NpgsqlParameter("@param","roleid");
            _command.CommandText = command;
            _command.Parameters.Add(roleParam);
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    switch (reader.GetString(2).ToLower())
                    {
                        case "admin":
                            users.Add(new Admin(reader.GetString(0),reader.GetString(1)));
                            break;
                        case "salesman":
                            users.Add(new Salesman(reader.GetString(0), reader.GetString(1)));
                            break;
                        case "manager":
                            users.Add(new Manager(reader.GetString(0), reader.GetString(1)));
                            break;
                        default:
                            throw new Exception("Роль не существует!");
                    }
                }       
            }
            reader.Close();
            return users;
        }

        public override void Create(User model)
        {
            throw new Exception("Нельзя создавать новые роли");
        }

        public override void Update(User model)
        {
            throw new Exception("Нельзя изменять роли");
        }

        public override void Delete(User model)
        {
            throw new Exception("Нельзя удалять роли");
        }       

    }
}
