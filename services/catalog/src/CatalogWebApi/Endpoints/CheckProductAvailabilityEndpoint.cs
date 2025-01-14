using Application.Queries.CheckProductAvailability;
using MediatR;

namespace CatalogWebApi.Endpoints
{
    public static class CheckProductAvailabilityEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/catalog/products/{productId:int}/availability", async (IMediator mediator, int productId, int quantity) =>
            {
                var result = await mediator.Send(new CheckProductAvailabilityQuery(productId, quantity));
                return result ? Results.Ok(true) : Results.NotFound(false);
            });
        }
    }
}
