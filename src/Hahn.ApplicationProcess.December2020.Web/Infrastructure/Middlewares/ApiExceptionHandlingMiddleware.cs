using Hahn.ApplicationProcess.December2020.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.Middlewares
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string result;

            switch (ex)
            {
                case NotFoundException:
                    {
                        var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]> { { "Error", new[] { ex.Message } } })
                        {
                            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                            Title = "Validation error occurred.",
                            Status = (int)HttpStatusCode.BadRequest,
                            Instance = context.Request.Path,
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize(problemDetails);
                        break;
                    }
                case ValidationException e:
                    {
                        var problemDetails = new ValidationProblemDetails(e.Errors)
                        {
                            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                            Title = "One or more validation errors occurred.",
                            Status = (int)HttpStatusCode.BadRequest,
                            Instance = context.Request.Path,
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize(problemDetails);
                        break;
                    }
                default:
                    {
                        _logger.LogError(ex, $"An unhandled exception has occurred, {ex.Message}");
                        var problemDetails = new ProblemDetails
                        {
                            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                            Title = "Internal Server Error.",
                            Status = (int)HttpStatusCode.InternalServerError,
                            Instance = context.Request.Path,
                            Detail = "Internal Server Error!"
                        };
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        result = JsonSerializer.Serialize(problemDetails);
                        break;
                    }
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}