using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("CatalogServiceClient", client =>
            {
                client.BaseAddress = new Uri(configuration["CatalogService:BaseUrl"]);
            });

            services.AddHttpClient("PaymentServiceClient", client =>
            {
                client.BaseAddress = new Uri(configuration["PaymentService:BaseUrl"]);
            });

            services.AddScoped<CatalogServiceClient>();
            services.AddScoped<PaymentServiceClient>();
        }
    }
}
