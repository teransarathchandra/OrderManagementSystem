using Infrastructure.Persistence;
using MediatR;

namespace Application.Catalog.Retrieve.CheckProductAvailability
{
    public class CheckProductAvailabilityQueryHandler(CatalogDbContext dbContext) : IRequestHandler<CheckProductAvailabilityQuery, bool>
    {
        public async Task<bool> Handle(CheckProductAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(request.ProductId);

            if (product == null)
                return false;

            return product.AvailableQuantity >= request.Quantity;
        }
    }
}
