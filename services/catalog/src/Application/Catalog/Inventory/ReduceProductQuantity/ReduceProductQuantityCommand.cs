using MediatR;

namespace Application.Catalog.Inventory.ReduceProductQuantity
{
    public class ReduceProductQuantityCommand(Guid productId, int quantity) : IRequest<bool>
    {
        public Guid ProductId { get; } = productId;
        public int Quantity { get; } = quantity;
    }
}
