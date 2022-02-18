using Catalog.Data;
using Catalog.Entities;
using Catalog.Helpers;
using Catalog.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.ExtensionServices
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection applicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
    }
}
