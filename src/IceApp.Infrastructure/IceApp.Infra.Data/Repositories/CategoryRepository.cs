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
using System.Linq;


namespace IceApp.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private string _connect;
        public CategoryRepository(IConfiguration connectionString)
        {           
            _connect = connectionString.GetConnectionString("DefaultConnection");
        }
        
        public async Task<IEnumerable<Category>> GetCategories()
        {
            
            using (var db=new NpgsqlConnection(_connect))
            {
                var cat = await db.QueryAsync<Category>("Select * from Categories where ParentId is null");
                return cat;
            }
            
        }
        public async Task<Category> GetById(int id)
        {

            using (var db = new NpgsqlConnection(_connect))
            {
                Category cat = await db.QueryFirstAsync<Category>($"select * from categories where id={id}");

                return cat;
            }

        }

        public async  Task<IEnumerable<Category>> GetSubcategories(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                var scat = await db.QueryAsync<Category>($"Select * from Categories where ParentId={id}");
                return scat;
            }
        }

        public  void Remove(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"DELETE FROM Categories WHERE Id = {id};");
                
            }
        }

        public void Add(Category categ)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                //await db.ExecuteAsync($"INSERT INTO Categories (Name,Image) VALUES ('{categ.Name}','{categ.Image}');");
                 db.Execute("INSERT INTO Categories (Name,Image) Values (@Name, @Image);", categ);
            }
        }

        public void Update(Category categ)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Categories SET Name=@Name, Image=@Image WHERE Id=@Id;",categ);
            }
        }
        public void UpdateDiscount(int discount,int categId)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Categories SET Discount={discount} WHERE Id={categId};");
            }
        }
        public void ResetDiscount(int categId)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Categories SET Discount=NULL WHERE Id={categId};");
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
