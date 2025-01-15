using Application.Queries.GetProductsByCategory;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    public static class GetProductsByCategoryEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/catalog/products/by-category/{categoryId:int}", async (IMediator mediator, int categoryId) =>
            {
                var result = await mediator.Send(new GetProductsByCategoryQuery(categoryId));
                return result.Any() ? Results.Ok(result) : Results.NotFound($"No products found for category ID {categoryId}.");
            });
        }
    }
}