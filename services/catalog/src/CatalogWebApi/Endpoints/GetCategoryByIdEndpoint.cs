using Application.Queries.GetCategoryById;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    public static class GetCategoryByIdEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/categories/{categoryId:int}", async (IMediator mediator, int categoryId) =>
            {
                var result = await mediator.Send(new GetCategoryByIdQuery(categoryId));
                return result != null ? Results.Ok(result) : Results.NotFound($"Category with ID {categoryId} not found.");
            });
        }
    }
}