using Domain.Models;
using MediatR;

namespace Application.Catalog.Retrieve.GetProductById
{
    public class GetProductByIdQuery(Guid productId) : IRequest<Product>
    {
        public Guid ProductId { get; } = productId;
    }
}
