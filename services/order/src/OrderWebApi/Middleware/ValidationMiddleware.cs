using System.Text.Json;
using FluentValidation;

namespace OrderWebApi.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<RequiresValidationAttribute>() != null)
            {
                var dtoType = endpoint.Metadata.GetMetadata<RequiresValidationAttribute>()?.DtoType;
                if (dtoType != null)
                {
                    context.Request.EnableBuffering(); // Enable buffering to allow multiple reads
                    try
                    {
                        // Check content type
                        if (!context.Request.ContentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) ?? true)
                        {
                            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                            await context.Response.WriteAsync("Unsupported content type. Please use 'application/json'.");
                            return;
                        }

                        // Deserialize body
                        var body = await JsonSerializer.DeserializeAsync(context.Request.Body, dtoType, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (body == null)
                        {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync("Invalid or missing request body.");
                            return;
                        }

                        // Validate
                        var validatorType = typeof(IValidator<>).MakeGenericType(dtoType);
                        var validator = context.RequestServices.GetService(validatorType);
                        if (validator is IValidator validatorInstance)
                        {
                            var validationResult = await validatorInstance.ValidateAsync(new ValidationContext<object>(body));
                            if (!validationResult.IsValid)
                            {
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                await context.Response.WriteAsJsonAsync(new
                                {
                                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                                });
                                return;
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Error = "Invalid JSON format.",
                            Details = ex.Message
                        });
                        return;
                    }
                    finally
                    {
                        context.Request.Body.Position = 0; // Reset the body position for downstream middleware
                    }
                }
            }

            await _next(context);
        }
    }

    public class RequiresValidationAttribute : Attribute
    {
        public Type DtoType { get; }

        public RequiresValidationAttribute(Type dtoType)
        {
            DtoType = dtoType;
        }
    }
}