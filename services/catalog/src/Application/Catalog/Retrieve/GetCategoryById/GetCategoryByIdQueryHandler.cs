using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalog.Retrieve.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(CatalogDbContext dbContext) : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);
        }
    }
}
