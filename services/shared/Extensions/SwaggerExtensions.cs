using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Shared.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddCustomSwagger(this IServiceCollection services, string apiTitle)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = apiTitle,
                    Version = "v1",
                    Description = $"API for managing {apiTitle.ToLower()}",
                    Contact = new OpenApiContact
                    {
                        Name = "Teran Sarathchandra",
                        Email = "teran8777@gmail.com"
                    }
                });
            });
        }

        public static void UseCustomSwagger(this WebApplication app, string apiTitle)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{apiTitle} V1");
                    options.RoutePrefix = string.Empty;
                });
            }
        }
    }
}