﻿using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.ReduceProductQuantity
{
    public class ReduceProductQuantityHandler : IRequestHandler<ReduceProductQuantityCommand, bool>
    {
        private readonly CatalogDbContext _dbContext;

        public ReduceProductQuantityHandler(CatalogDbContext dbContext)
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