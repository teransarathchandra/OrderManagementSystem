using Application.Customer.Create;
using MediatR;
using Shared.Middleware;

namespace CustomerWebApi.Endpoints
{
    internal static class RegisterCustomerEndpoint
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
