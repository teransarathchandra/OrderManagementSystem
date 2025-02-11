using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalog.Retrieve.SearchProducts
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<Product>>
    {
        private readonly CatalogDbContext _dbContext;

        public SearchProductsQueryHandler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{request.Query}%") ||
                            EF.Functions.Like(p.Description, $"%{request.Query}%"))
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);
        }
    }
}