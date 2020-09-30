﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IceApp.Domain.Models;

namespace IceApp.Application.Interfaces
{
    interface ISubcategorySevice
    {
        Task<Category> GetById(int id);
        Task<IEnumerable<Category>> GetSubcategories(int id);
        void Remove(int id);
        void Add(Category category);

        void Update(Category category);
    }
}