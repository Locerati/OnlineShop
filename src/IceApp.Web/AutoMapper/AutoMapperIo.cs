using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace IceApp.Web.AutoMapper
{
    public class AutoMapperIo
    {
        public static void RegisterMappings(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelToDomainProfile());
                cfg.AddProfile(new DomainToViewModelProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
