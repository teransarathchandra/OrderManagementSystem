using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Shared.Extensions
{
    public static class TelemetryExtensions
    {
        public static IServiceCollection AddCustomOpenTelemetry(this IServiceCollection services, string serviceName, IConfiguration configuration)
        {
            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    tracerProviderBuilder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault()
                            .AddService(serviceName)
                            .AddAttributes(new Dictionary<string, object>
                            {
                                ["environment"] = configuration["ASPNETCORE_ENVIRONMENT"],
                                ["region"] = "us-east-1"
                            }))
                        .AddAspNetCoreInstrumentation(options =>
                        {
                            options.RecordException = true;
                        })
                        .AddHttpClientInstrumentation()
                        .AddJaegerExporter(jaegerOptions =>
                        {
                            jaegerOptions.AgentHost = configuration["Jaeger:Host"] ?? "localhost";
                            jaegerOptions.AgentPort = int.Parse(configuration["Jaeger:Port"] ?? "6831");
                        });
                })
                .WithMetrics(metricsProviderBuilder =>
                {
                    metricsProviderBuilder
                        .AddAspNetCoreInstrumentation()
                        .AddPrometheusExporter();
                });

            return services;
        }
    }
}
