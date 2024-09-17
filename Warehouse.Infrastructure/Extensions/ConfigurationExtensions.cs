using Microsoft.Extensions.DependencyInjection;
using Warehouse.Domain.Abstractions;
using Warehouse.Infrastructure.Persistence.Repositories;

namespace Warehouse.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddPersistence();
            
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services) 
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
