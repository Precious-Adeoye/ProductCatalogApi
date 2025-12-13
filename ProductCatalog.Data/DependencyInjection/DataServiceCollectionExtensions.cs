using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data.Context;

namespace ProductCatalog.Data.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddData(
              this IServiceCollection services,
              IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
