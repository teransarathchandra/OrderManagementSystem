using MediatR;

namespace Application.Commands.ReduceProductQuantity
{
    public class ReduceProductQuantityCommand : IRequest<bool>
    {
        public int ProductId { get; }
        public int Quantity { get; }

        public ReduceProductQuantityCommand(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}