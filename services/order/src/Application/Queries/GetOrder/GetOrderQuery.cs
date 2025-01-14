using Domain.Models;
using MediatR;

namespace Application.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
        public int OrderId { get; }

        public GetOrderQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}