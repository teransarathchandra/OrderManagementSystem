using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalog.Retrieve.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly CatalogDbContext _dbContext;

        public GetCategoriesQueryHandler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.ToListAsync(cancellationToken);
        }
    }
}