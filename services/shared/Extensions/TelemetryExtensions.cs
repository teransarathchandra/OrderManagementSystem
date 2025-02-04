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
            var otelExporterEndpoint = configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] ?? "http://aspire-dashboard:18889";

            services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource.AddService(serviceName);
                    resource.AddTelemetrySdk();
                    resource.AddAttributes(new Dictionary<string, object>
                    {
                        ["deployment.environment"] = configuration["ASPNETCORE_ENVIRONMENT"] ?? "Production",
                        ["service.name"] = serviceName,
                        ["service.version"] = "1.0.0"
                    });
                })
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddProcessInstrumentation();

                    metrics.AddOtlpExporter(options => options.Endpoint = new Uri(otelExporterEndpoint));
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation(options =>
                        {
                            options.RecordException = true;
                        })
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation()
                        .AddSource(serviceName);

                    tracing.AddOtlpExporter(options => options.Endpoint = new Uri(otelExporterEndpoint));
                });

            return services;
        }
    }
}