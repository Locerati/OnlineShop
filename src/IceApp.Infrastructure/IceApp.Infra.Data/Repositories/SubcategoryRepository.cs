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
    public class SubcategoryRepository:ISubcategoriesRepository
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
                var scat = await db.QueryAsync<Category>($"Select * from Categories where ParentId={id}");
                return scat;
            }
        }

        public async void Remove(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                await db.ExecuteAsync($"DELETE FROM Categories WHERE Id = {id};");

            }
        }

        public async void Add(Category categ)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                await db.ExecuteAsync($"INSERT INTO Categories (Name,Image) VALUES ('{categ.Name}','{categ.Image}');");
            }
        }

        public async void Update(Category categ)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                await db.ExecuteAsync($"UPDATE Categories SET Name='{categ.Name}', Image='{categ.Image}' WHERE Id={categ.Id};");
            }
        }
    }
}
