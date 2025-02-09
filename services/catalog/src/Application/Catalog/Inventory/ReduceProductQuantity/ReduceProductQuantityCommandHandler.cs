using Infrastructure.Persistence;
using MediatR;

namespace Application.Catalog.Inventory.ReduceProductQuantity
{
    public class ReduceProductQuantityCommandHandler : IRequestHandler<ReduceProductQuantityCommand, bool>
    {
        private readonly CatalogDbContext _dbContext;

        public ReduceProductQuantityCommandHandler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(ReduceProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FindAsync(request.ProductId);

            if (product == null || product.AvailableQuantity < request.Quantity)
                return false;

            product.AvailableQuantity -= request.Quantity;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
