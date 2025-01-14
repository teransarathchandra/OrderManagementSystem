using Application.Commands.CancelOrder;
using MediatR;

namespace OrderWebApi.Endpoints
{
    public static class CancelOrderEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapDelete("/orders/{orderId}", async (IMediator mediator, int orderId) =>
            {
                var result = await mediator.Send(new CancelOrderCommand(orderId));
                return result ? Results.Ok($"Order with ID {orderId} has been canceled.") : Results.NotFound($"Order with ID {orderId} not found.");
            });
        }
    }
}