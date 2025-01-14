using Application.Queries.GetProductById;
using CatalogWebApi.Endpoints;
using CatalogWebApi.Middleware;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductByIdHandler).Assembly));

var app = builder.Build();

// Apply migrations and update the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.Database.Migrate();
}

// Add exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

// Map endpoints
EndpointMappings.MapEndpoints(app);

app.Run();