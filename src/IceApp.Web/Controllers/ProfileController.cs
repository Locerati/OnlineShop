using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using IceApp.Application.Interfaces;
using IceApp.Web.Models;
using AutoMapper;
using IceApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private IWebHostEnvironment _appEnvironment;
        private IEncryptionService _encryptionService;
        public ProfileController(IUserService userService, IMapper mapper, IWebHostEnvironment appEnviroment,IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            _appEnvironment = appEnviroment;
            _userService = userService;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            UserModel user = new UserModel();
            user = await _userService.GetUserByEmail(User.Identity.Name);
            UserViewModel userView = _mapper.Map<UserViewModel>(user);
            if (userView.Image.Length == 0)//если изображения нет устанавливаем исходное
            {
                string path = "/images/badges/account.png";
                userView.Image = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + path);

            }
            userView.Address = _encryptionService.Decrypt(userView.Address);//расшифровываем данные
            userView.PhoneNumber = _encryptionService.Decrypt(userView.PhoneNumber);
            return View(userView);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(IFormFile files)
        {
            if (files != null)
            {
                int userid = await _userService.GetUserIdByEmail(User.Identity.Name);
                byte[] image;

                using (var binaryReader = new BinaryReader(files.OpenReadStream()))
                {
                    image = binaryReader.ReadBytes((int)files.Length);
                }

                _userService.UpdateImage(image, userid);
                return NoContent();
            }
            else
            {
                return BadRequest("Не указаны параметры запроса");

            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, IFormFile file)
        {
            UserModel user = new UserModel();
            user = await _userService.GetUserByEmail(User.Identity.Name);
            
            if (ModelState.IsValid)
            {
                user.UserName = model.UserName;
                user.Address = _encryptionService.Encrypt(model.Address);
                user.PhoneNumber = _encryptionService.Encrypt(model.PhoneNumber);
                _userService.UpdateWithoutImage(user);
            }
            else
            {
                ModelState.AddModelError("", "Неккоректные данные");
            }
            if (user.Image.Length == 0)
            {
                string path = "/images/badges/account.png";
                model.Image = System.IO.File.ReadAllBytes(_appEnvironment.WebRootPath + path);
            }
            else
                model.Image = user.Image;

            return View("Index",model);
        }
    }
}
