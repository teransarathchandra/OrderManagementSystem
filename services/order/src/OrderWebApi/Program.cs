using System.Reflection;
using FluentValidation;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OrderWebApi.Endpoints;
using Serilog;
using Shared.Extensions;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.ConfigureSerilog();

// Add Logging with OpenTelemetry
builder.Logging.AddCustomLogging("OrderWebApi");

// Add DbContext
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HTTP Clients
builder.Services.AddCustomHttpClients(builder.Configuration);

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

// Register FluentValidation Validators
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Application"), includeInternalTypes: true);

// Add OpenTelemetry (Shared)
builder.Services.AddCustomOpenTelemetry("OrderWebApi", builder.Configuration);

// Add Swagger
builder.Services.AddCustomSwagger("Order API");

var app = builder.Build();

// Add Global Middleware
app.UseGlobalMiddlewares();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}
// Log HTTP requests
app.UseSerilogRequestLogging();

// Enable Swagger middleware
app.UseCustomSwagger("Order API");

// Map endpoints
EndpointMappings.MapEndpoints(app);

app.Run();
