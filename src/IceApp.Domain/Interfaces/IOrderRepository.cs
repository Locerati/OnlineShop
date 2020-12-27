using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.ChildModels;
using IceApp.Domain.Models;


namespace IceApp.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public int TotalOrderPrice(int userid);
        public void Add(Order order, int userid);
        public Task<Order> Get(int id);
        public Task<IEnumerable<Order>> GetOrdersByUserId(int id);
        public Task<IEnumerable<OrderInfo>> GetProductByOrderId(int orderid);
        public Task<IEnumerable<Order>> GetAll();
        public double TotalPriceWithSale(int userid);
    }
}
