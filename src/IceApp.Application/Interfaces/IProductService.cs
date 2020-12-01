using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;

namespace IceApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetProductsByCategory(int id);
        void Remove(int id);
        void Add(Product product);
        void UpdateWithoutImg(Product product);
        void Update(Product product);
        Task<string> GetParentName(int id);
        Task<IEnumerable<ProductCountComments>> GetProductsWithContComments(int parentId);
    }
}
