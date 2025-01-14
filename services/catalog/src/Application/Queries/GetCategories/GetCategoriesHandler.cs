using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly CatalogDbContext _dbContext;

        public GetCategoriesHandler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.ToListAsync(cancellationToken);
        }
    }
}