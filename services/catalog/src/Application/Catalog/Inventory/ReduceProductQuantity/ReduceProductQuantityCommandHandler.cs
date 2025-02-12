using Infrastructure.Persistence;
using MediatR;

namespace Application.Catalog.Inventory.ReduceProductQuantity
{
    public class ReduceProductQuantityCommandHandler(CatalogDbContext dbContext)
        : IRequestHandler<ReduceProductQuantityCommand, bool>
    {
        public async Task<bool> Handle(ReduceProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(request.ProductId);

            if (product == null || product.AvailableQuantity < request.Quantity)
                return false;

            product.AvailableQuantity -= request.Quantity;
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
