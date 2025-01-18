using MediatR;

namespace Application.Commands.ReduceProductQuantity
{
    public class ReduceProductQuantityCommand : IRequest<bool>
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public ReduceProductQuantityCommand(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}