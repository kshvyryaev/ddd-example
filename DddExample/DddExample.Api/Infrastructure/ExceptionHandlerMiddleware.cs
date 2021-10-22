using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DddExample.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DddExample.Api.Infrastructure
{
    public class ExceptionHandlerMiddleware
    {
        private const string ContentType = "application/json; charset=utf-8";

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (CustomExceptionBase ex)
            {
                await HandleCustomExceptionAsync(context, ex);
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                _logger.LogError(ex, ex.Message);
            }
        }

        private static async Task HandleCustomExceptionAsync(HttpContext context, CustomExceptionBase customException)
        {
            var bodyJson = JsonSerializer.Serialize(customException.Response);

            context.Response.StatusCode = customException.StatusCode;
            context.Response.ContentType = ContentType;
            await context.Response.WriteAsync(bodyJson, Encoding.UTF8);
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var body = new[] { new ExceptionResponse { Message = ex.Message } };
            var bodyJson = JsonSerializer.Serialize(body);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = ContentType;
            await context.Response.WriteAsync(bodyJson, Encoding.UTF8);
        }
    }
}