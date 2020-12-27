using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Linq;
using IceApp.Domain.ChildModels;
using IceApp.Domain;
namespace IceApp.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private string _connect;
        public OrderRepository(IConfiguration connectionString)
        {
            _connect = connectionString.GetConnectionString("DefaultConnection");
        }
        public int TotalOrderPrice(int userid)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                IEnumerable<dynamic> sum = db.Query<dynamic>("select sum(baskets.quantity*products.price) as Sum from products " +
                                                     "inner join baskets on products.id = baskets.productid " +
                                                     $"where baskets.userid = {userid};");
                int a = Convert.ToInt32(sum.First().sum);

                return a;
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
        public async void Add(Order order, int userid)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                int id = db.QuerySingle<int>($"INSERT INTO orders (address,deliverymethod,paymentmethod,totalprice,orderdate,userid) Values (@Address,@DeliveryMethod,@PaymentMethod,@TotalPrice,@OrderDate,{userid}) RETURNING id;", order);
                await db.ExecuteAsync(@$" INSERT INTO orderitems(productid, quantity, orderid)
                          (SELECT productid, quantity, {id} FROM baskets WHERE userid = {userid});");
                await db.ExecuteAsync($"DELETE FROM baskets WHERE userid = {userid};");
            }
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.QueryAsync<Order>($"select * from orders where userid={id};");
            }
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.QueryAsync<Order>($"select * from orders;");
            }
        }
        public async Task<Order> Get(int id)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.QueryFirstOrDefaultAsync<Order>($"select * from orders where  id={id};");
            }

        }
        public async Task<IEnumerable<OrderInfo>> GetProductByOrderId(int orderid)
        {
            using (var db = new NpgsqlConnection(_connect))
            {
                return await db.QueryAsync<OrderInfo>(@$"select  * from products
                                                            inner join orderitems as o on products.id = o.productid
                                                            where o.orderid={orderid}");
            }

        }


    }
}
