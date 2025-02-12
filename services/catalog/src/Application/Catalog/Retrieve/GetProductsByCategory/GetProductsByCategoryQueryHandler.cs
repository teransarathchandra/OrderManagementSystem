using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalog.Retrieve.GetProductsByCategory
{
    public class GetProductsByCategoryQueryHandler(CatalogDbContext dbContext)
        : IRequestHandler<GetProductsByCategoryQuery, List<Product>>
    {
        public async Task<List<Product>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Where(p => p.CategoryId == request.CategoryId)
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);
        }
    }
}
