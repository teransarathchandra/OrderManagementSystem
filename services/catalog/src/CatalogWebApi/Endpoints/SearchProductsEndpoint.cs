using Application.Catalog.Retrieve.SearchProducts;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    internal static class SearchProductsEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/catalog/products/search", async (IMediator mediator, string query) =>
            {
                var result = await mediator.Send(new SearchProductsQuery(query));
                return result.Any() ? Results.Ok(result) : Results.NotFound($"No products found matching '{query}'.");
            });
        }
    }
}
