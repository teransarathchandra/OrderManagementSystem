using MediatR;

namespace Application.Order.Delete
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; }

        public CancelOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
