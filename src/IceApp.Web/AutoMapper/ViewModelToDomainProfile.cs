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
            CreateMap<RegisterViewModel, UserModel>()
                .ForMember(m=>m.UserName,opt=>opt.MapFrom(c=>c.Surname+' '+c.Name+' '+c.Patronymic));
            CreateMap<RegisterViewModel, UserIdentity >();
            CreateMap<BasketItemViewModel, Basket>();
            CreateMap<UserViewModel, UserModel>();
        }
    }
}
