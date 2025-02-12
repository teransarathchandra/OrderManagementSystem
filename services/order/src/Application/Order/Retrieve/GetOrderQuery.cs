using Domain.Models;
using MediatR;

namespace Application.Order.Retrieve
{
    public class GetOrderQuery(Guid orderId) : IRequest<Domain.Models.Order>
    {
        public Guid OrderId { get; } = orderId;
    }
}
