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

        void Update(Category category);
    }
}
