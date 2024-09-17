
using Microsoft.Extensions.DependencyInjection;

namespace Warehouse.Application.Extensions
{
    public static class ConfigationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            // Add services to the container.
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(typeof(ConfigationExtensions).Assembly);
            });

            return services;
        }
    }
}
