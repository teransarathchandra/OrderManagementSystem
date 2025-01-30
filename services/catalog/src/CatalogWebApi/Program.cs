using System.Reflection;
using CatalogWebApi.Endpoints;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Extensions;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

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

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.WithEnvironmentName()
    .Enrich.WithThreadId()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add OpenTelemetry
builder.Services.AddCustomOpenTelemetry("CatalogWebApi", builder.Configuration);

var app = builder.Build();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.Database.Migrate();
}

// Add exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

// Log HTTP requests
app.UseSerilogRequestLogging();

// Expose Prometheus metrics endpoint
app.UseOpenTelemetryPrometheusScrapingEndpoint();

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