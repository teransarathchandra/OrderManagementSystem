using Domain.Models;
using MediatR;

namespace Application.Order.Retrieve
{
    public class GetOrderQuery : IRequest<Domain.Models.Order>
    {
        public Guid OrderId { get; }

        public GetOrderQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
