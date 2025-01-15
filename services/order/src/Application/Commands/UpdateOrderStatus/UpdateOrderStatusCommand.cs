using Domain.Models;
using MediatR;

namespace Application.Commands.UpdateOrderStatus
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