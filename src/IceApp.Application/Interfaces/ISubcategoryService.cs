using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.Models;

namespace IceApp.Application.Interfaces
{
    public interface ISubcategoryService
    {
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetSubcategories(int id);
        void Remove(int id);
        void Add(Category category);
        void UpdateWithoutImg(Category category);
        void Update(Category category);
        Task<string> GetParentName(int id);
    }
}
