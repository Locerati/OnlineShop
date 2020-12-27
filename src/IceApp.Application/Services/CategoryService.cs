using IceApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using IceApp.Domain.Interfaces;
using IceApp.Domain.Models;
using System.Threading.Tasks;

namespace IceApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public ICategoryRepository _categorieRepository;

        public CategoryService(ICategoryRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        public void Add(Category category)
        {
            _categorieRepository.Add(category);
        }
        public void UpdateDiscount(int discount, int categId)
        {
            _categorieRepository.UpdateDiscount(discount, categId);
        }
        public async Task<Category> GetById(int id)
        {
            return await _categorieRepository.GetById(id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
           
            return await _categorieRepository.GetCategories(); 
            
        }
        public void ResetDiscount(int categId)
        {
            _categorieRepository.ResetDiscount(categId);
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
  

