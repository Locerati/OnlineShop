using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IceApp.Application.Interfaces;
using IceApp.Web.Models;
using AutoMapper;
using IceApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    public class SubcategoriesController : Controller
    {
        private ICategoryService _categories;
        IWebHostEnvironment _appEnvironment;
        public SubcategoriesController(ICategoryService categorieService, IWebHostEnvironment appEnvironment)
        {
            _categories = categorieService;
            _appEnvironment = appEnvironment;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>());
            var mapper = new Mapper(config);
            var categoryViews = mapper.Map<IEnumerable<CategoryViewModel>>(await _categories.GetCategories());

            return View(categoryViews);
        }

        public async Task<IActionResult> List()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>());
            var mapper = new Mapper(config);
            var categoryViews = mapper.Map<IEnumerable<CategoryViewModel>>(await _categories.GetCategories());

            return View(categoryViews);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model, IFormFile uploadedFile)
        {
            if (ModelState.IsValid && uploadedFile != null)
            {

                string path = "/Content/storeimages/categories/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                model.Image = path;

                var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryViewModel, Category>());
                var mapper = new Mapper(config);
                var category = mapper.Map<Category>(model);
                _categories.Add(category);

                return RedirectToAction("List");
            }

            else
                return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>());
            var mapper = new Mapper(config);
            var category = mapper.Map<CategoryViewModel>(await _categories.GetById(id));
            if (category != null)
                return View(category);
            else
                return NotFound();
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {

            _categories.Remove(id);
            return RedirectToAction("List");

        }
        public async Task<IActionResult> Edit(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>());
            var mapper = new Mapper(config);
            var category = mapper.Map<CategoryViewModel>(await _categories.GetById(id));
            if (category != null)
                return View(category);
            else
                return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    string path = "/Content/storeimages/categories/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    model.Image = path;
                }


                var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryViewModel, Category>());
                var mapper = new Mapper(config);
                var category = mapper.Map<Category>(model);
                _categories.Update(category);

                return RedirectToAction("List");
            }

            else
                return View(model);
        }
    }
}
