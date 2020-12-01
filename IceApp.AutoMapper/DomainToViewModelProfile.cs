using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IceApp.Web.Models;
using IceApp.Domain.Models;

namespace IceApp.AutoMapper
{
    public class DomainToViewModelProfile: Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, SubcategoryViewModel>();
        }
    }
}
