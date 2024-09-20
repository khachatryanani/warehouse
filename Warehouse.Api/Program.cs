using Warehouse.Infrastructure.Persistence.Options;
using Warehouse.Infrastructure.Extensions;
using Warehouse.Application.Extensions;
using FluentValidation.AspNetCore;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using System.Reflection;
using Warehouse.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection(MongoDbOptions.Section));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationRulesToSwagger();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
