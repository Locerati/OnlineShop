using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using IceApp.Application.Interfaces;
using IceApp.Domain.ChildModels;

namespace IceApp.Application.Services
{
    public class ProductService:IProductService
    {
        public IProductRepository   _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Add(Product product)
        {
            _productRepository.Add(product);
        }

        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(int id)
        {
            return await _productRepository.GetProductsByCategory(id);
        }
        public async Task<string> GetParentName(int id)
        {
            return await _productRepository.GetParentName(id);
        }

        public void Remove(int id)
        {
            _productRepository.Remove(id);
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);
        }
        public void UpdateWithoutImg(Product Product)
        {
            _productRepository.UpdateWithoutImg(Product);
        }
        public async Task<IEnumerable<ProductCountComments>> GetProductsWithContComments(int parentId)
        {
            return await _productRepository.GetProductsWithContComments(parentId);
        }

       
    }
}
