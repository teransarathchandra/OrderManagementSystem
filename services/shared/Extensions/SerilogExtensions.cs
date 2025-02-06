using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Shared.Extensions
{
    public static class SerilogExtensions
    {
        public static void ConfigureSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.WithEnvironmentName()
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.OpenTelemetry(options =>
                {
                    options.Endpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] ?? "http://aspire-dashboard:18889";
                })
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}