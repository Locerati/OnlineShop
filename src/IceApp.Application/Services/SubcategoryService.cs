using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;
using IceApp.Application.Interfaces;

namespace IceApp.Application.Services
{
    public class SubcategoryService:ISubcategoryService
    {
        public ISubcategoryRepository _categorieRepository;

        public SubcategoryService(ISubcategoryRepository scategorieRepository)
        {
            _categorieRepository = scategorieRepository;
        }
        public async Task<string> GetParentName(int id)
        {
           return await _categorieRepository.GetParentName(id);
        }

        public void Add(Category category)
        {
            _categorieRepository.Add(category);
        }

        public async Task<Category> GetById(int id)
        {
            return await _categorieRepository.GetById(id);
        }

        public async Task<IEnumerable<Category>> GetSubcategories(int id)
        {
            return await _categorieRepository.GetSubcategories( id);
        }

        public void Remove(int id)
        {
            _categorieRepository.Remove(id);
        }

        public void Update(Category category)
        {
            _categorieRepository.Update(category);
        }
        public void UpdateWithoutImg(Category category)
        {
            _categorieRepository.UpdateWithoutImg(category);
        }
    }
}
