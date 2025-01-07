using System.Net;
using System.Text.Json;
using OrbitMap.API.Payload.Response;
using OrbitMap.API.Payload.Response.Result;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        // var errorResponse = new ErrorResponse() { TimeStamp = DateTime.UtcNow, Error = exception.Message };
        var errorResponse = new ApiErrorResult<string>();
        switch (exception)
        {
            //add more custom exception
            //For example case AppException: do something
            case BadHttpRequestException badRequestException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                // errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Errors = new List<string> { badRequestException.Message };
                errorResponse.Message = "Bad request.";
                _logger.Error(badRequestException, "Bad request error");
                break;
            default:
                //unhandled error
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                // errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Errors = new List<string> { exception.Message };
                errorResponse.Message = "An unexpected error occurred.";
                _logger.Error(exception, "Unhandled exception");
                break;
        }
        var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(result);
    }
}