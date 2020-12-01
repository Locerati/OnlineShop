using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IceApp.Web.Models;
using IceApp.Domain.Models;

namespace IceApp.Web.AutoMapper
{
   
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<CategoryViewModel, Category>();
            CreateMap< SubcategoryViewModel, Category>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<CommentViewModel, Comment>();
        }
    }
}
