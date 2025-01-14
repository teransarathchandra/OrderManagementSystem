using MediatR;

namespace Application.Queries.CheckProductAvailability
{
    public class CheckProductAvailabilityQuery : IRequest<bool>
    {
        public int ProductId { get; }
        public int Quantity { get; }

        public CheckProductAvailabilityQuery(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}