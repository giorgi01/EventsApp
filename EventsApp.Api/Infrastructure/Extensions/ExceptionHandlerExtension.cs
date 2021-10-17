using EventsApp.Api.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EventsApp.Api.Infrastructure.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
