using Infrastructure.Persistence;
using MediatR;

namespace Application.Queries.CheckProductAvailability
{
    public class CheckProductAvailabilityHandler : IRequestHandler<CheckProductAvailabilityQuery, bool>
    {
        private readonly CatalogDbContext _dbContext;

        public CheckProductAvailabilityHandler(CatalogDbContext dbContext)
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
