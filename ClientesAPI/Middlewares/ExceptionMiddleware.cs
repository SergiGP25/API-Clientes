using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ClientesAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // continua con la petición
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Error de negocio");
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, dex.Message);
            }
            catch (KeyNotFoundException knf)
            {
                _logger.LogWarning(knf, "Recurso no encontrado");
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, knf.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = (int)statusCode,
                error = message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
