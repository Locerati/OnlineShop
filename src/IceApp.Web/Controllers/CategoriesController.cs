using System;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryService _categories;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categorieService,  IMapper mapper)
        {
            _mapper = mapper;
            _categories = categorieService;
       
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var categoryViews = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categories.GetCategories());
            return View(categoryViews);
        }

        public async Task<IActionResult> List()
        {            
            var categoryViews = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categories.GetCategories());
            return View(categoryViews);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel model, IFormFile formFile)
        {
            if (ModelState.IsValid && formFile != null)
            {             
                using (var binaryReader=new BinaryReader(formFile.OpenReadStream()))
                {
                    model.Image =binaryReader.ReadBytes((int)formFile.Length);
                }
                var category = _mapper.Map<Category>(model);                
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
            var category = _mapper.Map<CategoryViewModel>(await _categories.GetById(id));
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
        public async Task<IActionResult> Edit (int id)
        {            
            var category = _mapper.Map<CategoryViewModel>(await _categories.GetById(id));
            if (category != null)
                return View(category);
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(CategoryViewModel model, IFormFile formFile)
        {

            if (ModelState.IsValid)
            {
                if (formFile != null)
                {
                    
                    using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
                    {
                        model.Image = binaryReader.ReadBytes((int)formFile.Length);
                    }
                    var category = _mapper.Map<Category>(model);
                    _categories.Update(category);

                }
                else
                {
                    var category = _mapper.Map<Category>(model);
                    _categories.UpdateWithoutImg(category);

                }
                
              

                return RedirectToAction("List");
            }

            else
                return View(model);
        }

    }
}
