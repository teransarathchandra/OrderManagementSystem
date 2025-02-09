using Domain.Models;
using MediatR;

namespace Application.Catalog.Retrieve.GetProductsByCategory
{
    public class GetProductsByCategoryQuery : IRequest<List<Product>>
    {
        public Guid CategoryId { get; }

        public GetProductsByCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}