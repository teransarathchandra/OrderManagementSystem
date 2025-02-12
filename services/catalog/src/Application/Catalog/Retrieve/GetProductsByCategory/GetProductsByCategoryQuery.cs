using Domain.Models;
using MediatR;

namespace Application.Catalog.Retrieve.GetProductsByCategory
{
    public class GetProductsByCategoryQuery(Guid categoryId) : IRequest<List<Product>>
    {
        public Guid CategoryId { get; } = categoryId;
    }
}
