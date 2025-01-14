using System.Text.Json;

namespace CatalogWebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Process the next middleware in the pipeline
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception details (optional: use a logging library like Serilog)
            Console.WriteLine($"Error: {exception.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new
            {
                Error = "An error occurred while processing your request.",
                Details = exception.Message // Omit this for production to avoid exposing sensitive details
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}