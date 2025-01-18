using Domain.Models;
using MediatR;

namespace Application.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid ProductId { get; }

        public GetProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }
}