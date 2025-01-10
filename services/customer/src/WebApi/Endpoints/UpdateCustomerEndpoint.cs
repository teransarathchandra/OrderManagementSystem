using Application.Commands.updateCustomer;
using Application.DTOs;
using FluentValidation;
using MediatR;
using WebApi.Middleware;

namespace WebApi.Endpoints
{
    public static class UpdateCustomerEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPut("/customers/{customerId}", async (IMediator mediator, int customerId, UpdateCustomerDto customerDto) =>
            {
                var result = await mediator.Send(new UpdateCustomerCommand(customerId, customerDto));
                if (result == null)
                {
                    return Results.NotFound($"Customer with ID {customerId} not found.");
                }

                return Results.Ok(result);
            })
            .WithMetadata(new RequiresValidationAttribute(typeof(UpdateCustomerDto)));
        }
    }
}
