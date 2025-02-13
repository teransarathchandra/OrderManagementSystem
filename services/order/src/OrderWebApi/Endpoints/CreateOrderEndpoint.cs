using Application.Order.Create;
using MediatR;
using Shared.Middleware;

namespace OrderWebApi.Endpoints
{
    internal static class CreateOrderEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/orders", async (IMediator mediator, CreateOrderDto orderDto) =>
                {
                    var result = await mediator.Send(new CreateOrderCommand(orderDto));
                    return Results.Created($"/orders/{result.Id}", result);
                })
                .WithMetadata(new RequiresValidationAttribute(typeof(CreateOrderDto)));
        }
    }
}
