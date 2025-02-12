using MediatR;

namespace Application.Order.Delete
{
    public class CancelOrderCommand(Guid orderId) : IRequest<bool>
    {
        public Guid OrderId { get; } = orderId;
    }
}
