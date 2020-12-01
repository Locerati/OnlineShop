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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productserv;
        private ICommentService _commentService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productservervice, ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
            _productserv = productservervice;

        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(int scategoryId)
        {
            var productsList = new ProductsListViewModel();
            productsList.products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productserv.GetProductsWithContComments(scategoryId));
            productsList.ParentName =await _productserv.GetParentName(scategoryId);
            return View(productsList);
        }

        public async Task<IActionResult> List(int scategoryId)
        {
            
            var productsList = new ProductsListViewModel();
            var mmd = await _productserv.GetProductsByCategory(scategoryId);
            productsList.products = _mapper.Map<IEnumerable<ProductViewModel>>(mmd);
            productsList.CategoryId= scategoryId;
            return View(productsList);
        }
        [HttpGet]
        public IActionResult Create(int categoryId)
        {
            return View(new ProductViewModel {CategoryId=categoryId});
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model, IFormFile formFile,int days,int hours)
        {
            if (ModelState.IsValid && formFile != null)
            {
                using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
                {
                    model.Image = binaryReader.ReadBytes((int)formFile.Length);
                }
                model.DeliveryPeriod = TimeSpan.FromDays(days).Ticks + TimeSpan.FromHours(hours).Ticks;
                var product = _mapper.Map<Product>(model);
                _productserv.Add(product);
                return RedirectToAction("List", new { scategoryId = model.CategoryId });
            }
            else
                return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productserv.GetById(id));
            if (product != null)
                return View(product);
            else
                return NotFound();
        }


        [HttpPost]
        public IActionResult Delete(int id, int ParentId)
        {
            _productserv.Remove(id);
            return RedirectToAction("List", new { scategoryId = ParentId });

        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productserv.GetById(id));
            if (product != null)
                return View(product);
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel model, IFormFile formFile, int days, int hours)
        {
            if (ModelState.IsValid)
            {
                model.DeliveryPeriod = TimeSpan.FromDays(days).Ticks + TimeSpan.FromHours(hours).Ticks;
                if (formFile != null)
                {

                    using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
                    {
                        model.Image = binaryReader.ReadBytes((int)formFile.Length);
                    }
                    var product = _mapper.Map<Product>(model);
                    _productserv.Update(product);
                }
                else
                {
                    var product = _mapper.Map<Product>(model);
                    _productserv.UpdateWithoutImg(product);

                }

                return RedirectToAction("List", new { scategoryId = model.CategoryId });
            }

            else
                return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productserv.GetById(id));   
            //productInfo.MainInformation= _mapper.Map<ProductViewModel>(await _productserv.GetById(id));
            //productInfo.CountOfComments = await _commentService.GetCountByProductId(id);
            return View(product);
        }
       
        

    }
}
