using Domain.Models;
using MediatR;

namespace Application.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
        public Guid OrderId { get; }

        public GetOrderQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}