using System.Reflection;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderWebApi.Endpoints;
using OrderWebApi.Middleware;
using Serilog;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

// Register FluentValidation Validators
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"), includeInternalTypes: true);

builder.Services.AddHttpClient<CatalogServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CatalogService:BaseUrl"]);
});

builder.Services.AddHttpClient<PaymentServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PaymentService:BaseUrl"]);
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order API",
        Version = "v1",
        Description = "API for managing orders in the system",
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

builder.Services.AddCustomOpenTelemetry("OrderWebApi", builder.Configuration);

var app = builder.Build();

// Add Validation Middleware
app.UseMiddleware<ValidationMiddleware>();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
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
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
        options.RoutePrefix = string.Empty;
    });
}

// Map endpoints
EndpointMappings.MapEndpoints(app);

app.Run();