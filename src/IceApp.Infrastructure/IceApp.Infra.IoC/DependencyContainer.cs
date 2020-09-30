﻿using System;
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
        }
    }
}