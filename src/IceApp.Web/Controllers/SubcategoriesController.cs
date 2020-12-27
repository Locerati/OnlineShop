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
using Microsoft.AspNetCore.Authorization;

namespace IceApp.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class SubcategoriesController : Controller
    {
        private ISubcategoryService _scategories;
        private readonly IMapper _mapper;
        public SubcategoriesController(ISubcategoryService scategorieService, IMapper mapper)
        {
            _mapper = mapper;
            _scategories = scategorieService;

        }
        // GET: /<controller>/
        [AllowAnonymous]
        public async Task<IActionResult> Index(int categoryId)
        {
            var subcategoriesList = new SubcategoriesListViewModel();
            subcategoriesList.subcategories = _mapper.Map<IEnumerable<SubcategoryViewModel>>(await _scategories.GetSubcategories(categoryId));
            subcategoriesList.CategoryName = await _scategories.GetParentName(categoryId);
            subcategoriesList.ParentId = categoryId;
            return View(subcategoriesList);
        }
        //Список подкатегорий для админа
        public async Task<IActionResult> List(int categoryId)
        {
            var subcategoriesList =new SubcategoriesListViewModel();
            subcategoriesList.subcategories = _mapper.Map<IEnumerable<SubcategoryViewModel>>(await _scategories.GetSubcategories(categoryId));
            subcategoriesList.CategoryName = await _scategories.GetParentName(categoryId);
            subcategoriesList.ParentId = categoryId;
            return View(subcategoriesList); 
        }
        [HttpGet]
        public IActionResult Create(int ParentId)
        {

            return View(new SubcategoryViewModel { ParentId=ParentId} );
        }
        [HttpPost]
        public  IActionResult Create(SubcategoryViewModel model, IFormFile formFile)
        {
            if (ModelState.IsValid && formFile != null)
            {
                using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
                {
                    model.Image = binaryReader.ReadBytes((int)formFile.Length);
                }
                var scategory = _mapper.Map<Category>(model);
                _scategories.Add(scategory);
                return RedirectToAction("List",new {categoryId=model.ParentId });
            }
            else
                return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var scategory = _mapper.Map<SubcategoryViewModel>(await _scategories.GetById(id));
            if (scategory != null)
                return View(scategory);
            else
                return NotFound();
        }


        [HttpPost]
        public IActionResult Delete(int id, int ParentId)
        {
            _scategories.Remove(id);
             return RedirectToAction("List", new { categoryId = ParentId });

        }
        public async Task<IActionResult> Edit(int id)
        {
            var scategory = _mapper.Map<SubcategoryViewModel>(await _scategories.GetById(id));
            if (scategory != null)
                return View(scategory);
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(SubcategoryViewModel model, IFormFile formFile)
        {
           if (ModelState.IsValid)
            {
                if (formFile != null)
                {

                    using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
                    {
                        model.Image = binaryReader.ReadBytes((int)formFile.Length);
                    }
                    var scategory = _mapper.Map<Category>(model);
                    _scategories.Update(scategory);
                }
                else
                {
                    var scategory = _mapper.Map<Category>(model);
                    _scategories.UpdateWithoutImg(scategory);

                }

                return RedirectToAction("List", new { categoryId = model.ParentId });
            }

            else
                return View(model);
        }


    }
}