using Infrastructure.Persistence;

namespace WebApi.Endpoints
{
    public static class DeleteCustomerEndpoint
    {
        public static void MapDeleteCustomerEndpoint(this WebApplication app)
        {
            app.MapDelete("/customers/{customerId}", async (CustomerDbContext db, int customerId) =>
            {
                var customer = await db.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return Results.NotFound($"Customer with ID {customerId} not found.");
                }

                db.Customers.Remove(customer);
                await db.SaveChangesAsync();

                return Results.Ok($"Customer with ID {customerId} has been deleted.");
            });
        }
    }
}
