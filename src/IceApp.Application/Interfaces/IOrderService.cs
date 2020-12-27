using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;

namespace IceApp.Application.Interfaces
{
    public interface IOrderService
    {
        public int TotalOrderPrice(int userid);
        public double TotalPriceWithSale(int userid);
        public  void Add(Order order, int userid);
        public Task<Order> Get(int id);
        public Task<IEnumerable<Order>> GetOrdersByUserId(int id);
        public Task<IEnumerable<OrderInfo>> GetProductByOrderId(int orderid);
        public Task<IEnumerable<Order>> GetAll();
    }
}
