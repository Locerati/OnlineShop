using IceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IceApp.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetById(int id);
        void Add(Category categ);
        void Update(Category categ);
        public void UpdateDiscount(int discount, int categId);
        public void ResetDiscount(int categId);
        void UpdateWithoutImg(Category category);
        void Remove(int id);
    }
}
