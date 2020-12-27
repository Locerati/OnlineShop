using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IceApp.Application.Interfaces;
using AutoMapper;
using IceApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using IceApp.Web.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using IceApp.Domain.ChildModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class BasketController : Controller
    {
        private IBasketService _basketServ;
        private IUserService _userService;
        private readonly IMapper _mapper;
        public BasketController(IBasketService basketServervice,IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _basketServ = basketServervice;
            _userService = userService;
        }

        
        public async Task< IActionResult> Index()
        {
            var userid = await _userService.GetUserIdByEmail(HttpContext.User.Identity.Name);
            BasketViewModel basketView = new BasketViewModel();
            basketView.BasketsItems = await _basketServ.GetProductsByUserId(userid);
            foreach (var i in basketView.BasketsItems)
            {
                basketView.TotalPrice += i.Price*i.Quantity;
                basketView.CountOfProducts += i.Quantity;
            }
            basketView.PriceWithSale =(decimal) _basketServ.TotalPriceWithSale(userid);
            return View(basketView);
        }


        public async void AddProduct(BasketItemViewModel basketItem)
        {
            basketItem.Quantity = basketItem.Quantity == 0 ? 1 : basketItem.Quantity;  //Если не получили количество продуктов, то присваиваем автоматически 1
            int userid= await _userService.GetUserIdByEmail(User.Identity.Name);
            var basketId = _basketServ.GetBasketId(userid,basketItem.ProductId);
            if (basketId != null)
            {
                _basketServ.UpdateQuentity((int)basketId,basketItem.Quantity);  //Если в корзине уже есть такой продукт обновляем количество товара
            }
            else
            {
                var item = _mapper.Map<Basket>(basketItem); 
                item.UserId = userid;
                _basketServ.Add(item); //Иначе добовляем новый продукт
            }
            
        }

        public async Task<BasketUpdateInfo> DeleteItem(int basketId)
        {
            var userId = await _userService.GetUserIdByEmail(User.Identity.Name);
            _basketServ.Delete(userId, basketId);
            var updateItem = _basketServ.TotalOrderPrice(userId);//Получаем сумму без скидки
            updateItem.DiscountSum = _basketServ.TotalPriceWithSale(userId);//Сумма с учетом скидки
            return updateItem ; 
        }
    }
}
