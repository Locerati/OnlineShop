using IceApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using IceApp.Domain.ChildModels;

namespace IceApp.Application.Services
{
    public class BasketService:IBasketService
    {
        public IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public void Add(Basket item)
        {
            _basketRepository.Add(item);
        }
        public async Task<IEnumerable<BasketInfo>> GetProductsByUserId(int id)
        {
            return await _basketRepository.GetProductsByUserId(id);
        }
        public  BasketUpdateInfo TotalOrderPrice(int userid)
        {
            return _basketRepository.TotalOrderPrice(userid);
        }
        public double TotalPriceWithSale(int userid)
        {
            return _basketRepository.TotalPriceWithSale(userid);
        }
        public void Delete(int userid, int basketid)
        {
            _basketRepository.Delete(userid, basketid);
        }
        public int? GetBasketId(int userid, int productid)
        {
           return _basketRepository.GetBasketId(userid, productid);
        }
        public void UpdateQuentity(int basketid, int quentity)
        {
             _basketRepository.UpdateQuentity(basketid,quentity);
        }
    }
}
