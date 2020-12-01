using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace IceApp.Infra.Data.Repositories
{
    public class SubcategoryRepository:ISubcategoryRepository
    {
        private string _connect;
        public SubcategoryRepository(IConfiguration connectionString)
        {
            _connect = connectionString.GetConnectionString("DefaultConnection");
        }

       
        public async Task<Category> GetById(int id)
        {

            using (var db = new NpgsqlConnection(_connect))
            {
                Category cat = await db.QueryFirstAsync<Category>($"select * from categories where id={id}");

                return cat;
            }

        }

        public async Task<IEnumerable<Category>> GetSubcategories(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                var scat = await db.QueryAsync<Category>($"Select * from Categories where ParentId = {id}; ");
                return scat;
            }
        }
        public async Task<string> GetParentName(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                var scat = await db.QuerySingleOrDefaultAsync<string>($"Select Name from Categories where Id = {id}; ");
                return scat;
            }
        }

        public void Remove(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
               db.Execute($"DELETE FROM Categories WHERE Id = {id};");

            }
        }

        public  void Add(Category categ)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                 db.Execute("INSERT INTO Categories (Name,Image,ParentId) Values (@Name, @Image,@ParentId);", categ);
            }
        }

        public void Update(Category categ)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Categories SET Name=@Name, Image=@Image WHERE Id=@Id;", categ);
            }
        }
        public void UpdateWithoutImg(Category categ)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Categories SET Name=@Name WHERE Id=@Id;", categ);
            }
        }
    }
}
