using Hahn.ApplicationProcess.December2020.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
            => app.UseMiddleware<ApiExceptionHandlingMiddleware>();
    }
}