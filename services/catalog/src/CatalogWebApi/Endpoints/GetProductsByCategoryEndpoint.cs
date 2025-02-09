using Application.Catalog.Retrieve.GetProductsByCategory;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    public static class GetProductsByCategoryEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/catalog/products/by-category/{categoryId:Guid}", async (IMediator mediator, Guid categoryId) =>
            {
                var result = await mediator.Send(new GetProductsByCategoryQuery(categoryId));
                return result.Any() ? Results.Ok(result) : Results.NotFound($"No products found for category ID {categoryId}.");
            });
        }
    }
}
