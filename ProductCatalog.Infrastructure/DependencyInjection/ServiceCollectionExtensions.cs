using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Core.Contracts.Interface;
using ProductCatalog.DataAccess.Repositories.Implementation;
using ProductCatalog.DataAccess.Repositories.Interface;
using ProductCatalog.DataAccess.UnitOfWork.Implimentation;
using ProductCatalog.Infrastructure.IServices;
using ProductCatalog.Infrastructure.Services;

namespace ProductCatalog.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
