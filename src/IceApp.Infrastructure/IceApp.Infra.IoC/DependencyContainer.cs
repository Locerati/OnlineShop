using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using IceApp.Application.Interfaces;
using IceApp.Application.Services;
using IceApp.Domain.Interfaces;
using IceApp.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IceApp.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ISubcategoryService, SubcategoryService>();           
            services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEncryptionService, EncryptionService>();

            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IBasketRepository, BasketRepository>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();

        }
    }
}
