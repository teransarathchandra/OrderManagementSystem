using System.Reflection;
using CatalogWebApi.Endpoints;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Serilog;
using Shared.Extensions;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
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
    //.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

//// Add Logging with OpenTelemetry
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true; // Ensures logs have formatted messages
    logging.IncludeScopes = true; // Enables structured logging with scopes
    logging.ParseStateValues = true; // Ensures additional attributes are included in logs

    //Add ResourceBuilder with the same service name
    logging.SetResourceBuilder(
        ResourceBuilder
            .CreateDefault()
            .AddService(
                serviceName: "CatalogWebApi",
                serviceVersion: "1.0.0"
            )
    );

    logging.AddOtlpExporter(); // Sends logs to Aspire Dashboard (port 18889)
});

// Add DbContext
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

// Add OpenTelemetry (Shared)
builder.Services.AddCustomOpenTelemetry("CatalogWebApi", builder.Configuration);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "API for managing product catalog",
        Contact = new OpenApiContact
        {
            Name = "Teran Sarathchandra",
            Email = "teran8777@gmail.com"
        }
    });
});

//// Configure Serilog
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .Enrich.WithEnvironmentName()
//    .Enrich.WithThreadId()
//    .Enrich.FromLogContext()
//    .WriteTo.Console()
//    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();

//builder.Host.UseSerilog();

var app = builder.Build();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.Database.Migrate();
}

// Log HTTP requests
app.UseSerilogRequestLogging();

// Add exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
        options.RoutePrefix = string.Empty;
    });
}

// Map endpoints
EndpointMappings.MapEndpoints(app);

app.Run();