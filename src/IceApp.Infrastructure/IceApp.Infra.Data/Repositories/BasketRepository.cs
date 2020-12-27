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
using IceApp.Domain.ChildModels;
namespace IceApp.Infra.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private string _connect;
        public BasketRepository(IConfiguration connectionString)
        {
            _connect = connectionString.GetConnectionString("DefaultConnection");
        }
        public async void Add(Basket item)
        {
            using (var db = new NpgsqlConnection(_connect))
            {

                await db.ExecuteAsync("INSERT INTO Baskets (ProductId,UserId,Quantity) Values (@ProductId,@UserId,@Quantity);", item);
            }
        }
        public async void UpdateQuentity(int basketid,int quentity)
        {
            using (var db = new NpgsqlConnection(_connect))
            {

                await db.ExecuteAsync($"UPDATE baskets SET quantity = quantity + {quentity} where id={basketid}; ");
            }
        }
        public int? GetBasketId(int userid, int productid)
        {
            using (var db = new NpgsqlConnection(_connect))
            {

                var a= db.QuerySingleOrDefault($"select id from baskets where userid = {userid} and productid = {productid};");
                return a?.id;
            }
        }
        public async Task<IEnumerable<BasketInfo>> GetProductsByUserId(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.QueryAsync<BasketInfo>("select p.id as Id, p.name, p.description, p.weight, p.image, p.deliveryperiod, p.price, p.categoryid, baskets.id as BasketId, baskets.quantity  from products as p " +
                                                    "inner join baskets on p.id = baskets.productid " +
                                                    $"where baskets.userid = {id};");
            }
        }
        public BasketUpdateInfo TotalOrderPrice(int userid)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                IEnumerable<dynamic> sum =  db.Query<dynamic>("select sum(baskets.quantity*products.price) as Sum,sum(quantity) as Quantity from products " +
                                                     "inner join baskets on products.id = baskets.productid " +
                                                     $"where baskets.userid = {userid};");
                BasketUpdateInfo basket = new BasketUpdateInfo();
                basket.TotalSum = Convert.ToDouble(sum.First().sum);
                basket.TotalQuentity = Convert.ToInt32(sum.First().quantity);
                
                return basket;
            }

        }
        public double TotalPriceWithSale(int userid)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                IEnumerable<dynamic> sum = db.Query<dynamic>(@$"select sum(b.quantity * p.price * (100 - coalesce(c2.discount, 0)) / 100) as Sum from baskets as b
                                                                inner join products as p on p.id = b.productid
                                                                inner join categories as c on c.id = p.categoryid
                                                                inner join categories as c2 on c.parentid = c2.id
                                                                where b.userid =     { userid};");
               
                return Convert.ToDouble(sum.First().sum);
            }

        }
        public  void Delete(int userid, int basketid)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                db.Execute("DELETE FROM baskets " +
                                                     $"WHERE userid={userid} and id={basketid};");
            }
        }
    }
}
