using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalog.Retrieve.GetProductsByCategory
{
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, List<Product>>
    {
        private readonly CatalogDbContext _dbContext;

        public GetProductsByCategoryQueryHandler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Where(p => p.CategoryId == request.CategoryId)
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);
        }
    }
}