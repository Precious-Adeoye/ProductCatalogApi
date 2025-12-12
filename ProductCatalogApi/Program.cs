
using Microsoft.OpenApi.Models;
using ProductCatalog.Data.Context;
using ProductCatalog.Data.DependencyInjection;
using ProductCatalog.Data.Seeds;
using ProductCatalog.Infrastructure.DependencyInjection;


namespace ProductCatalogApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration & DI
            builder.Services.AddData(builder.Configuration);          // registers ApplicationDbContext
            builder.Services.AddInfrastructure();                     // registers repositories, services, UoW
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" }));


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                await SeedData.InitializeAsync(context, logger);
            }




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
