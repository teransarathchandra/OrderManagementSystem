using Domain.Models;
using MediatR;

namespace Application.Order.Update
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public Guid OrderId { get; }
        public OrderStatus Status { get; }

        public UpdateOrderStatusCommand(Guid orderId, OrderStatus status)
        {
            OrderId = orderId;
            Status = status;
        }
    }
}
