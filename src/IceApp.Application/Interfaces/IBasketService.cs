using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;

namespace IceApp.Application.Interfaces
{
    public interface IBasketService
    {
        public void Add(Basket item);
        public int? GetBasketId(int userid, int productid);
        public void UpdateQuentity(int basketid, int quentity);
        public double TotalPriceWithSale(int userid);
        public Task<IEnumerable<BasketInfo>> GetProductsByUserId(int id);
        public BasketUpdateInfo TotalOrderPrice(int userid);
        public void Delete(int userid, int basketid);
    }
}
