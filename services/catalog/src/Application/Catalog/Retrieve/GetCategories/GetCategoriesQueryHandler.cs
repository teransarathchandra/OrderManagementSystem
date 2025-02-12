using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalog.Retrieve.GetCategories
{
    public class GetCategoriesQueryHandler(CatalogDbContext dbContext) : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Categories.ToListAsync(cancellationToken);
        }
    }
}
