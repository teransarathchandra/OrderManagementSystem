using Application.Queries.CheckProductAvailability;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    public static class CheckProductAvailabilityEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/catalog/products/{productId:Guid}/availability", async (IMediator mediator, Guid productId, int quantity) =>
            {
                var result = await mediator.Send(new CheckProductAvailabilityQuery(productId, quantity));
                return result ? Results.Ok(true) : Results.NotFound(false);
            });
        }
    }
}