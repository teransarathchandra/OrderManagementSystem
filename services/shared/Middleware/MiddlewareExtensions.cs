using Microsoft.AspNetCore.Builder;

namespace Shared.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseGlobalMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ValidationMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}