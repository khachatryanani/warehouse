using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Abstractions.Services;
using Warehouse.Infrastructure.Consumers.Options;
using Warehouse.Infrastructure.Consumers.Sagas;
using Warehouse.Infrastructure.Persistence.Options;
using Warehouse.Infrastructure.Persistence.Repositories;
using Warehouse.Infrastructure.Services;

namespace Warehouse.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ConfigurationExtensions).Assembly);
            services.AddPersistence();
            services.AddServices();
            services.AddMassTransitOnRabbitMQ(configuration);


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
            services.AddScoped<IStockService, StockService>();

            return services;
        }

        private static IServiceCollection AddMassTransitOnRabbitMQ(this IServiceCollection services, IConfiguration configuration) 
        {
            var mongoDbOptions = configuration.GetSection(MongoDbOptions.Section).Get<MongoDbOptions>();
            var massTransitOptions = configuration.GetSection(MassTransitOptions.Section).Get<MassTransitOptions>();


            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddDelayedMessageScheduler();

                x.AddConsumers(typeof(ConfigurationExtensions).Assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Durable = true;
                    cfg.AutoStart = true;

                    cfg.Host(massTransitOptions.Host, massTransitOptions.Port, massTransitOptions.VirtualHost, h =>
                    {
                        h.Username(massTransitOptions.Username);
                        h.Password(massTransitOptions.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });


                x.AddSagaStateMachine<OrderSubmittedFlow, OrderState>()
                .MongoDbRepository(mongoDbOptions.ConnectionString, x =>
                {
                    x.CollectionName = "order-submitted-flow";
                    x.DatabaseName = mongoDbOptions.DatabaseName;
                });
            });

            return services;
        }
    }
}
