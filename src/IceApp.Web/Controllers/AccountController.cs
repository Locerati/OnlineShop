using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IceApp.Web.Models;
using IceApp.Domain.Models;
using IceApp.Application.Interfaces;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private readonly IMapper _mapper;
        private IEncryptionService _encryptionService;
        public AccountController(IUserService userService,IMapper mapper, IEncryptionService encryptionService)
        {
            _mapper = mapper;
            _userService = userService;
            _encryptionService = encryptionService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdentity identity = await _userService.GetUserIdentity(model.Email);
                if (identity!=null && BC.Verify(model.Password,identity.Password))//Проверка пароля и наличие такого пользователя
                {
                    var user = await _userService.GetUserById(identity.UserId);
                    Role role = _userService.GetRoleById(user.RoleId);
                    await Authenticate(model.Email,role);  //Аунтификация пользователя в системе
                    return RedirectToAction("Index", "Home");  //Возвращаем на главную страницу

                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль"); //Еслипароль неверный или пользователя не существует добавляем ошибку

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdentity userIdentity = await _userService.GetUserIdentity(model.Email);
                if (userIdentity==null)
                {
                    var user = _mapper.Map<UserModel>(model);
                    user.Address= _encryptionService.Encrypt(user.Address); //шифрование данных
                    user.PhoneNumber = _encryptionService.Encrypt(user.PhoneNumber);
                    Role userRole = _userService.GetRoleByName("user");
                    if (userRole != null)
                    {
                        user.RoleId = userRole.Id;

                        int UserId = _userService.AddUser(user); //добавляем пользователя в бд


                        var identity = _mapper.Map<UserIdentity>(model);
                        identity.UserId = UserId;
                        identity.Password = BC.HashPassword(identity.Password); //хэшируем пароль 
                        _userService.AddIdentity(identity);  // добавление данных аунтификации пользователя в бд

                        await Authenticate(model.Email, userRole);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("","Неккоректные логин и(или) пароль");
                }
            }
            return View(model);
        }
        private async Task Authenticate(string userName,Role userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,userName),   //Email пользователя
                new Claim(ClaimsIdentity.DefaultRoleClaimType,userRole?.Name)  //Роль 
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        //Выход из системы
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
