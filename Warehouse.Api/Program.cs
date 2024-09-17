using Microsoft.AspNetCore.Hosting;
using Warehouse.Infrastructure.Persistence.Options;
using Warehouse.Infrastructure.Extensions;
using Warehouse.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(MongoDbOptions.Section));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
