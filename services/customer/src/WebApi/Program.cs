using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WebApi.Endpoints;
using FluentValidation;
using Application.Validators;
using Application.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerHandler).Assembly));

// Register FluentValidation Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();

var app = builder.Build();

// Map endpoints
app.MapRegisterCustomerEndpoint();
app.MapUpdateCustomerEndpoint();
app.MapRetrieveCustomerDetailsEndpoint();
app.MapDeleteCustomerEndpoint();

app.Run();