using Application.Queries.RetrieveCustomer;
using MediatR;

namespace CustomerWebApi.Endpoints
{
    public static class RetrieveCustomerDetailsEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/customers/{customerId}", async (IMediator mediator, int customerId) =>
            {
                var customer = await mediator.Send(new RetrieveCustomerQuery(customerId));
                if (customer == null)
                {
                    return Results.NotFound($"Customer with ID {customerId} not found.");
                }

                return Results.Ok(customer);
            });
        }
    }
}