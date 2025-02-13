using Application.Catalog.Retrieve.GetProductById;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    internal static class GetProductByIdEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/catalog/products/{productId:Guid}", async (IMediator mediator, Guid productId) =>
            {
                var result = await mediator.Send(new GetProductByIdQuery(productId));
                return result != null ? Results.Ok(result) : Results.NotFound($"Product with ID {productId} not found.");
            });
        }
    }
}
