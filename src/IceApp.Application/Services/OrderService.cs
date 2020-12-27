using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using IceApp.Domain.ChildModels;
using IceApp.Application;
using IceApp.Application.Interfaces;

namespace IceApp.Application.Services
{
    public class OrderService:IOrderService
    {
        public IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public int TotalOrderPrice(int userid)
        {
            return _orderRepository.TotalOrderPrice(userid);
        }
        public void Add(Order order, int userid)
        {
             _orderRepository.Add(order, userid);
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int id)
        {
           return await  _orderRepository.GetOrdersByUserId(id);
        }
        public async Task<Order> Get(int id)
        {
            return await _orderRepository.Get(id);
        }
        public async Task<IEnumerable<OrderInfo>> GetProductByOrderId(int orderid)
        {
            return await _orderRepository.GetProductByOrderId(orderid);
        }
        public double TotalPriceWithSale(int userid)
        {
            return _orderRepository.TotalPriceWithSale(userid);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await  _orderRepository.GetAll();
        }
    }
}
