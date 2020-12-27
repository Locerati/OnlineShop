using IceApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace IceApp.Infra.Data.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private string _connect;
        public ProductRepository(IConfiguration connectionString)
        {
            _connect = connectionString.GetConnectionString("DefaultConnection");
        }


        public async Task<Product> GetById(int id)
        {

            using (var db = new NpgsqlConnection(_connect))
            {
                Product product = await db.QueryFirstAsync<Product>($"select * from Products where id={id}");

                return product;
            }

        }
        public async Task<IEnumerable<ProductCountComments>> GetProductsWithContComments(int parentId)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                var product = await db.QueryAsync<ProductCountComments>("select P.Id,P.Name,P.Image,P.Price,count(C.Id) as CountOfComments from products as P " +
                                                                        "left join comments as C on C.productid = P.id "+
                                                                       $"where categoryid = {parentId} "+
                                                                        "group by P.Id, P.Name, P.price, P.Image ");
                return product;
            }

        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                var product = await db.QueryAsync<Product>($"Select * from Products where CategoryId = {id}; ");
                return product;
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
                db.Execute($"DELETE FROM Products WHERE Id = {id};");

            }
        }

        public void Add(Product product)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute("INSERT INTO Products (Name,Description,Weight,Image,Deliveryperiod,Price,CategoryId) Values (@Name,@Description,@Weight,@Image,@Deliveryperiod,@Price,@CategoryId);", product);
            }
        }

        public void Update(Product product)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Products SET Name=@Name,Description=@Description,Weight=@Weight,Image=@Image,Deliveryperiod=@Deliveryperiod,Price=@Price,CategoryId=@CategoryId WHERE Id=@Id;", product);
            }
        }
        public void UpdateWithoutImg(Product product)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute($"UPDATE Products SET Name=@Name,Description=@Description,Weight=@Weight,Deliveryperiod=@Deliveryperiod,Price=@Price,CategoryId=@CategoryId WHERE Id=@Id;", product);
            }
        }
        
    }
}
