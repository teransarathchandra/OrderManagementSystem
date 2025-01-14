using Application.Commands.CreateOrder;
using Application.DTOs;
using CustomerWebApi.Middleware;
using MediatR;

namespace OrderWebApi.Endpoints
{
    public static class CreateOrderEndpoint
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