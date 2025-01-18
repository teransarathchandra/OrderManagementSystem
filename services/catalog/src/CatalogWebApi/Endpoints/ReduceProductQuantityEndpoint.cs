using Application.Commands.ReduceProductQuantity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogWebApi.Endpoints
{
    public static class ReduceProductQuantityEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/catalog/products/{productId:Guid}/reduce", async (IMediator mediator, Guid productId, [FromQuery] int quantity) =>
            {
                var result = await mediator.Send(new ReduceProductQuantityCommand(productId, quantity));
                return result ? Results.Ok("Quantity reduced successfully.") : Results.BadRequest("Failed to reduce product quantity.");
            });
        }
    }
}