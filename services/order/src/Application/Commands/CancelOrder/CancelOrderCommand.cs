using MediatR;

namespace Application.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public int OrderId { get; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}