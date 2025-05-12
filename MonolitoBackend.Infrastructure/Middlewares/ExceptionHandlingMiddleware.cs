using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MonolitoBackend.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        var errorId = Guid.NewGuid().ToString();
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new
        {
            status = GetStatusCode(exception),
            error = GetErrorMessage(exception),
            timestamp = DateTime.UtcNow,
            errorId = errorId,
            path = context.Request.Path,
            method = context.Request.Method
        };

        response.StatusCode = errorResponse.status;
        
        _logger.LogError(exception, 
            "Error ID: {ErrorId} | Path: {Path} | Method: {Method} | Error: {Message}", 
            errorId, 
            context.Request.Path, 
            context.Request.Method, 
            exception.Message);
        
        await response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ArgumentException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            NotSupportedException => StatusCodes.Status501NotImplemented,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetErrorMessage(Exception exception)
    {
        return exception switch
        {
            UnauthorizedAccessException => "Acesso não autorizado",
            ArgumentException => "Requisição inválida",
            ValidationException => "Dados inválidos",
            KeyNotFoundException => "Recurso não encontrado",
            InvalidOperationException => "Operação inválida",
            NotSupportedException => "Operação não suportada",
            _ => "Erro interno no servidor"
        };
    }
}

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
} 