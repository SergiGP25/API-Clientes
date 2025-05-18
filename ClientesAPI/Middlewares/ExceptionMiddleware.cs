using Domain.Exceptions;
using FluentValidation;
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
                await _next(context);
            }
            catch (ValidationException vex) // FluentValidation
            {
                _logger.LogInformation(vex, "Error de validación");

                var errorList = vex.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                await WriteResponseAsync(context, HttpStatusCode.BadRequest, errorList);
            }
            catch (DomainException dex)
            {
                _logger.LogInformation(dex, "Error de dominio");

                await WriteResponseAsync(context, HttpStatusCode.BadRequest, new List<string> { dex.Message });
            }
            catch (KeyNotFoundException knf)
            {
                _logger.LogInformation(knf, "Recurso no encontrado");

                await WriteResponseAsync(context, HttpStatusCode.NotFound, new List<string> { knf.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");

                await WriteResponseAsync(context, HttpStatusCode.InternalServerError, new List<string> { "Ocurrió un error inesperado." });
            }
        }

        private async Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, List<string> errors)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                status = (int)statusCode,
                errors
            });

            await context.Response.WriteAsync(result);
        }
    }
}
