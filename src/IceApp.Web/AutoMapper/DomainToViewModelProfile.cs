using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using IceApp.Web.Models;
using IceApp.Domain.Models;
using IceApp.Domain.ChildModels;

namespace IceApp.Web.AutoMapper
{
    public class DomainToViewModelProfile: Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, SubcategoryViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductCountComments, ProductViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentsWithInfo, CommentViewModel>();
        }
    }
}
