using MediatR;

namespace Application.Catalog.Retrieve.CheckProductAvailability
{
    public class CheckProductAvailabilityQuery : IRequest<bool>
    {
        public Guid ProductId { get; }
        public int Quantity { get; }

        public CheckProductAvailabilityQuery(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}