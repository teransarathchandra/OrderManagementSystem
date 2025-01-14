using Application.Queries.ListCustomerOrders;
using MediatR;

namespace OrderWebApi.Endpoints
{
    public static class ListCustomerOrdersEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/customers/{customerId}/orders", async (IMediator mediator, int customerId) =>
            {
                var orders = await mediator.Send(new ListCustomerOrdersQuery(customerId));
                return orders.Any() ? Results.Ok(orders) : Results.NotFound($"No orders found for customer ID {customerId}.");
            });
        }
    }
}