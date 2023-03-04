using NLog;
using NLog.Fluent;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ExamenPractico.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        //Captura de las requests y manejo de excepciones
        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await ErrorResponse(context);
            }                
        }

        //Generar response con codigo de error y mensaje en el body
        private static Task ErrorResponse(HttpContext context)
        {
            var result = JsonSerializer.Serialize("Internal Server Error");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
