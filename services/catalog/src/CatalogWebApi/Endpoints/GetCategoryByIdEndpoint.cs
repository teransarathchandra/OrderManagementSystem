using Application.Catalog.Retrieve.GetCategoryById;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    internal static class GetCategoryByIdEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/categories/{categoryId:Guid}", async (IMediator mediator, Guid categoryId) =>
            {
                var result = await mediator.Send(new GetCategoryByIdQuery(categoryId));
                return result != null ? Results.Ok(result) : Results.NotFound($"Category with ID {categoryId} not found.");
            });
        }
    }
}
