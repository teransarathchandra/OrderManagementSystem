using Application.Commands.CreateCustomer;
using Application.Validators;
using CustomerWebApi.Endpoints;
using CustomerWebApi.Middleware;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerHandler).Assembly));

// Register FluentValidation Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();

var app = builder.Build();

// Add Validation Middleware
app.UseMiddleware<ValidationMiddleware>();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    dbContext.Database.Migrate();
}

// Map endpoints
EndpointMappings.MapEndpoints(app);

app.Run();