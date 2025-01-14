using Application.Queries.GetProductById;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    public static class GetProductByIdEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/catalog/products/{productId:int}", async (IMediator mediator, int productId) =>
            {
                var result = await mediator.Send(new GetProductByIdQuery(productId));
                return result != null ? Results.Ok(result) : Results.NotFound($"Product with ID {productId} not found.");
            });
        }
    }
}