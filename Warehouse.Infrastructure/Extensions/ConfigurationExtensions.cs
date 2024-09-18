using Microsoft.Extensions.DependencyInjection;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Infrastructure.Persistence.Repositories;

namespace Warehouse.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ConfigurationExtensions).Assembly);
            services.AddPersistence();
            services.AddServices();
            
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services) 
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStockService, IStockService>();

            return services;
        }
    }
}
