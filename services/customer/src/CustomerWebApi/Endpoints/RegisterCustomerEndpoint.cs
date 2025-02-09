﻿using Application.Commands.CreateCustomer;
using MediatR;
using Shared.Middleware;

namespace CustomerWebApi.Endpoints
{
    public static class RegisterCustomerEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/customers/register", async (IMediator mediator, CreateCustomerDto customerDto) =>
            {
                var result = await mediator.Send(new CreateCustomerCommand(customerDto));
                return Results.Created($"/customers/{result.Id}", result);
            })
            .WithMetadata(new RequiresValidationAttribute(typeof(CreateCustomerDto)));
        }
    }
}