using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Resources;
using InvalidOperationException = Warehouse.Domain.Exceptions.InvalidOperationException;
using ValidationException = FluentValidation.ValidationException;
using static System.Net.HttpStatusCode;
namespace Warehouse.Api.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILoggerProvider provider)
{
    private static readonly JsonSerializerOptions _opt = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var errorResponseModel = HandleMessage(ex);
            context.Response.StatusCode = errorResponseModel.StatusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsJsonAsync(errorResponseModel, _opt);
        }
    }

    private static ExceptionResponseDto HandleMessage(Exception exception)
    {
        var errorResponseModel = exception switch
        {
            ValidationException validationException => new ExceptionResponseDto((int)BadRequest, nameof(ErrorMessages.ValidationError), validationException.Errors.Select(x => x.ErrorMessage).ToList()),
            DataNotFoundException dataNoteFoundException => new ExceptionResponseDto((int)NotFound, nameof(DataNotFoundException), dataNoteFoundException.Message),
            InvalidStateException invalidStateException => new ExceptionResponseDto((int)BadRequest, nameof(InvalidStateException), invalidStateException.Message),
            InvalidOperationException invalidOperationException => new ExceptionResponseDto((int)BadRequest, nameof(InvalidOperationException), invalidOperationException.Message),
            _ => new ExceptionResponseDto((int)InternalServerError, nameof(InternalServerError), ErrorMessages.SomethingWentWrong)
        };

        return errorResponseModel;
    }
}
