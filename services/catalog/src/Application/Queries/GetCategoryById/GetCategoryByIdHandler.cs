using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly CatalogDbContext _dbContext;

        public GetCategoryByIdHandler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);
        }
    }
}