using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; // Vom adăuga logging simplu
using SimpleBlog.Core.Exceptions;   // Pentru NotFoundException
using System;
using System.Net;
using System.Text.Json; // Pentru serializare JSON
using System.Threading.Tasks;

namespace SimpleBlog.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Lasă request-ul să treacă la următorul middleware din pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Prinde excepția și gestioneaz-o
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Loghează excepția
            _logger.LogError(exception, "An unhandled exception has occurred.");

            HttpStatusCode statusCode;
            string message;

            // Personalizează răspunsul în funcție de tipul excepției
            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404
                    message = notFoundException.Message; // Folosim mesajul din excepția noastră custom
                    break;
                // Aici poți adăuga și alte cazuri pentru excepții specifice
                // case ValidationException validationException:
                //     statusCode = HttpStatusCode.BadRequest; // 400
                //     message = "Validation failed.";
                //     // Ai putea include și detalii despre erorile de validare
                //     break;
                default:
                    // Pentru orice altă excepție neașteptată, returnăm o eroare generică de server
                    statusCode = HttpStatusCode.InternalServerError; // 500
                    message = "An unexpected error occurred. Please try again later.";
                    // Într-un mediu de producție, nu ai vrea să expui detalii despre excepții interne
                    // decât dacă este explicit dorit și gestionat (ex: doar în Development).
                    // Pentru simplitate, acum trimitem un mesaj generic.
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            // Creează un obiect anonim pentru răspunsul JSON
            var errorResponse = new { error = message };
            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}