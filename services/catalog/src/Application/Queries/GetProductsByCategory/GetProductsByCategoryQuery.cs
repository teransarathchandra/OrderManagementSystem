using Domain.Models;
using MediatR;

namespace Application.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQuery : IRequest<List<Product>>
    {
        public int CategoryId { get; }

        public GetProductsByCategoryQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}