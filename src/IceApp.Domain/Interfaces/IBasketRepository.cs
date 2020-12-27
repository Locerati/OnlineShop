using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.ChildModels;
using IceApp.Domain.Models;

namespace IceApp.Domain.Interfaces
{
    public interface IBasketRepository
    {
        public void Add(Basket item);
        public int? GetBasketId(int userid, int productid);
        public void UpdateQuentity(int basketid, int quentity);
        public Task<IEnumerable<BasketInfo>> GetProductsByUserId(int id);
        public BasketUpdateInfo TotalOrderPrice(int userid);
        public double TotalPriceWithSale(int userid);
        public void Delete(int userid, int basketid);
    }
}
