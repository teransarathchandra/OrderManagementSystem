using Domain.Models;
using MediatR;

namespace Application.Order.Update
{
    public class UpdateOrderStatusCommand(Guid orderId, OrderStatus status) : IRequest<bool>
    {
        public Guid OrderId { get; } = orderId;
        public OrderStatus Status { get; } = status;
    }
}
