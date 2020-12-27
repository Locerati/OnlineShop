using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using IceApp.Domain.Interfaces;
using Microsoft.VisualBasic;

namespace IceApp.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connect;
        public UserRepository(IConfiguration connectionString)
        {
            _connect = connectionString.GetConnectionString("DefaultConnection");
        }

        public async Task<UserModel> GetUserById(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.QueryFirstOrDefaultAsync<UserModel>($"select * from users where id='{id}'");
            }

        }
        public async Task<UserIdentity> GetUserIdentity(string email)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                UserIdentity identity = await db.QueryFirstOrDefaultAsync<UserIdentity>($"select * from UsersIdentitys where Email='{email}';");

                return identity;
            }
        }
        public int AddUser(UserModel user)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                int id= db.QuerySingle<int>("INSERT INTO Users (UserName,Address,PhoneNumber,RoleId) Values (@UserName,@Address,@PhoneNumber,@RoleId) RETURNING id;", user);

                return id;
             }
        }
        public void AddIdentity(UserIdentity identity)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                 db.Execute("INSERT INTO UsersIdentitys (Email,Password,UserId) Values (@Email,@Password,@UserId);", identity);
            }
        }
        public Role GetRoleByName(string name)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return db.QueryFirstOrDefault<Role>($"select * from roles where name='{name}'");
            }
        }
        public Role GetRoleById(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return db.QueryFirstOrDefault<Role>($"select * from roles where id='{id}'");
            }
        }
        public async Task<int> GetUserIdByEmail(string email)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.ExecuteScalarAsync<int>($"select users.id from users inner join usersidentitys u on users.id = u.userid where email = '{email}';");
            }
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.QueryFirstAsync<UserModel>($"select users.id as id, username, address, phonenumber, image, roleid from users inner join usersidentitys u on users.id = u.userid where email = '{email}';");
            }
        }
        public async void UpdateImage(byte[] image,int userid )
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                await db.ExecuteAsync($"UPDATE Users SET Image=@Image WHERE Id={userid};",new {Image=image });
            }
        }
        public void UpdateWithoutImage(UserModel user)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Users SET UserName=@UserName,Address=@Address,PhoneNumber=@PhoneNumber WHERE Id=@Id;", user);
            }
        }
    }
}
