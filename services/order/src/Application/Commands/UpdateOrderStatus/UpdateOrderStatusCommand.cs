using MediatR;

namespace Application.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public int OrderId { get; }
        public string Status { get; }

        public UpdateOrderStatusCommand(int orderId, string status)
        {
            OrderId = orderId;
            Status = status;
        }
    }
}