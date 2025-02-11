using Application.Catalog.Retrieve.GetCategories;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    public static class GetCategoriesEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/categories", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCategoriesQuery());
                return result.Any() ? Results.Ok(result) : Results.NotFound("No categories found.");
            });
        }
    }
}
