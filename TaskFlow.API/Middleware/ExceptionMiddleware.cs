using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace TaskFlow.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une exception non gérée a été interceptée.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    DbUpdateException => (int)HttpStatusCode.Conflict, // ex: doublon Email
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var response = _env.IsDevelopment()
                    ? new ApiError
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message,
                        Details = ex.StackTrace
                    }
                    : new ApiError
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex switch
                        {
                            DbUpdateException => "Erreur de base de données (conflit ou violation de contrainte).",
                            KeyNotFoundException => "La ressource demandée est introuvable.",
                            UnauthorizedAccessException => "Accès refusé.",
                            ArgumentException => "Requête invalide.",
                            _ => "Une erreur interne est survenue sur le serveur."
                        }
                    };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

        private class ApiError
        {
            public int StatusCode { get; set; }
            public string Message { get; set; } = string.Empty;
            public string? Details { get; set; }
        }
    }
}
