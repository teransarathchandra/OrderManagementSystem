using System.Reflection;
using CustomerWebApi.Endpoints;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Extensions;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.ConfigureSerilog();

// Add Logging with OpenTelemetry
builder.Logging.AddCustomLogging("CustomerWebApi");

// Add DbContext
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

// Register FluentValidation Validators
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"), includeInternalTypes: true);

// Add OpenTelemetry (Shared)
builder.Services.AddCustomOpenTelemetry("CustomerWebApi", builder.Configuration);

// Add Swagger
builder.Services.AddCustomSwagger("Customer API");

var app = builder.Build();

// Add Global Middleware
app.UseGlobalMiddlewares();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    dbContext.Database.Migrate();
}

// Log HTTP requests
app.UseSerilogRequestLogging();

// Enable Swagger middleware
app.UseCustomSwagger("Customer API");

// Map endpoints
EndpointMappings.MapEndpoints(app);

app.Run();