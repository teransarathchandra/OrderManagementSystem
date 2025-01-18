using MediatR;

namespace Application.Commands.CancelOrder
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