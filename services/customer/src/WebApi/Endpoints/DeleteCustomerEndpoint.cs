﻿using Application.Commands.DeleteCustomer;
using MediatR;

namespace WebApi.Endpoints
{
    public static class DeleteCustomerEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapDelete("/customers/{customerId}", async (IMediator mediator, int customerId) =>
            {
                var result = await mediator.Send(new DeleteCustomerCommand(customerId));
                if (!result)
                {
                    return Results.NotFound($"Customer with ID {customerId} not found.");
                }

                return Results.Ok($"Customer with ID {customerId} has been deleted.");
            });
        }
    }
}
