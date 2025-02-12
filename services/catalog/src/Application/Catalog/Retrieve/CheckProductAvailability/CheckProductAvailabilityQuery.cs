using MediatR;

namespace Application.Catalog.Retrieve.CheckProductAvailability
{
    public class CheckProductAvailabilityQuery(Guid productId, int quantity) : IRequest<bool>
    {
        public Guid ProductId { get; } = productId;
        public int Quantity { get; } = quantity;
    }
}
