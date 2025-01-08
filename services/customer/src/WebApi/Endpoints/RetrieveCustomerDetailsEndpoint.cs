using Infrastructure.Persistence;

namespace WebApi.Endpoints
{
    public static class RetrieveCustomerDetailsEndpoint
    {
        public static void MapRetrieveCustomerDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/customers/{customerId}", async (CustomerDbContext db, int customerId) =>
            {
                var customer = await db.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return Results.NotFound($"Customer with ID {customerId} not found.");
                }

                return Results.Ok(customer);
            });
        }
    }
}
