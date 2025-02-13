using Application.Customer.Update;
using MediatR;
using Shared.Middleware;

namespace CustomerWebApi.Endpoints
{
    internal static class UpdateCustomerEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPut("/customers/{customerId}", async (IMediator mediator, Guid customerId, UpdateCustomerDto customerDto) =>
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
