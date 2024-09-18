using MassTransit;
using OrderReviewService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(c =>
{
    c.SetKebabCaseEndpointNameFormatter();
    c.AddConsumer<ReviewOrderConsumer>();

    c.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("amqp://root:example@localhost:5672/");

        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();

app.Run();
