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
            List<User> users = new List<User>();
            string command = "select * from users;";
            _command.CommandText = command;
            NpgsqlDataReader reader = _command.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    switch (reader.GetInt32(1))
                    {
                        case 1:
                            users.Add(new Admin(reader.GetInt32(0),1,reader.GetString(2),reader.GetString(3)));
                            break;
                        case 2:
                            users.Add(new Salesman(reader.GetInt32(0), 2, reader.GetString(2), reader.GetString(3)));
                            break;
                        case 3:
                            users.Add(new Manager(reader.GetInt32(0), 3, reader.GetString(2), reader.GetString(3)));
                            break;
                        default:
                            throw new Exception("оль не существует");
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

        public User GetUser(string name,string password,List<User> users)
        {
            var sortUser = from user in users
                           where user.UserName == name &&
                                 user.Password == password
                           select user;
           
            if (sortUser.First() != null )
                return sortUser.First();            
            else
                return null;

        }

    }
}
