using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNetCore.Authorization.Infrastructure;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IUserService _userService;
        private IOrderService _orderService;
        private readonly IMapper _mapper;
        private IEncryptionService _encryptionService;
        public OrdersController(IUserService userService,IOrderService orderService, IEncryptionService encryptionService,IMapper mapper)
        {
            _orderService = orderService;
            _encryptionService = encryptionService;
            _mapper = mapper;
            _userService = userService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            int userid = await _userService.GetUserIdByEmail(User.Identity.Name);
            OrdersViewModel ordersView =new OrdersViewModel();
            if (User.IsInRole("admin")) //Если админ возвращаем полный списко заказов
            {
                ordersView.OrderList = await _orderService.GetAll(); 
            }
            else
            {
                ordersView.OrderList = await _orderService.GetOrdersByUserId(userid);
            }
            
            return View(ordersView);
        }
        public async Task<IActionResult> Create(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.PaymentMethod = "Наличными"; //Пока проект не захостили и не привязали оплату доступно только наличными
            int userid = await _userService.GetUserIdByEmail(User.Identity.Name);
            order.TotalPrice =(decimal)_orderService.TotalPriceWithSale(userid); //Ещё раз подсчитываем сумму заказа
            _orderService.Add(order, userid);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int orderId)
        {
            var ordersView = new OrderInfoViewModel();
            Order order = await _orderService.Get(orderId);
            ordersView =_mapper.Map<OrderInfoViewModel>(order);
            ordersView.ProductsList=await _orderService.GetProductByOrderId(orderId); //Получаем список продуктов в нашей корзине
            ordersView.CountOfProducts = ordersView.ProductsList.Sum(u=>u.Quantity); //Вычисляем количество
            if (User.IsInRole("admin"))  //Админу добаляем дополнительные данные о пользователях
            {
                UserModel user = await _userService.GetUserById(order.UserId);
                ordersView.UserName = user.UserName;
                ordersView.Phone = _encryptionService.Decrypt(user.PhoneNumber);
            }
            return View(ordersView);
        }


    }
}
