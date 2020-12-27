using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.Models;


namespace IceApp.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetById(int id);
        void Remove(int id);
        void Add(Category category);
        public void UpdateDiscount(int discount, int categId);
        void Update(Category category);
        public void ResetDiscount(int categId);
        void UpdateWithoutImg(Category category);
        
    }
}
