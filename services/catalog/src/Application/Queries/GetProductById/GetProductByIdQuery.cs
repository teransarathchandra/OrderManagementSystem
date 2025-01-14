using Domain.Models;
using MediatR;

namespace Application.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int ProductId { get; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}