using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.Models;

namespace IceApp.Domain.Interfaces
{
    public interface ISubcategoryRepository
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
