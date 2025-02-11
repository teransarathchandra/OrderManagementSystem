using Infrastructure.Persistence;
using MediatR;

namespace Application.Catalog.Retrieve.CheckProductAvailability
{
    public class CheckProductAvailabilityQueryHandler : IRequestHandler<CheckProductAvailabilityQuery, bool>
    {
        private readonly CatalogDbContext _dbContext;

        public CheckProductAvailabilityQueryHandler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(CheckProductAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FindAsync(request.ProductId);

            if (product == null)
                return false;

            return product.AvailableQuantity >= request.Quantity;
        }
    }
}