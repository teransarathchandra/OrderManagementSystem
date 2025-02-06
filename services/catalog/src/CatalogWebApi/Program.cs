using System.Reflection;
using CatalogWebApi.Endpoints;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Extensions;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.ConfigureSerilog();

// Add Logging with OpenTelemetry
builder.Logging.AddCustomLogging("CatalogWebApi");

// Add DbContext
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

// Add OpenTelemetry (Shared)
builder.Services.AddCustomOpenTelemetry("CatalogWebApi", builder.Configuration);

// Add Swagger
builder.Services.AddCustomSwagger("Catalog API");

var app = builder.Build();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.Database.Migrate();
}

// Log HTTP requests
app.UseSerilogRequestLogging();

// Add Global Middleware
app.UseGlobalMiddlewares();

// Enable Swagger middleware
app.UseCustomSwagger("Catalog API");

// Map endpoints
EndpointMappings.MapEndpoints(app);

app.Run();