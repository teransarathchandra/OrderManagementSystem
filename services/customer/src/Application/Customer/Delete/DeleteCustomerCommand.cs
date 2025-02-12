using MediatR;

namespace Application.Customer.Delete
{
    public class DeleteCustomerCommand(Guid customerId) : IRequest<bool>
    {
        public Guid CustomerId { get; } = customerId;
    }
}
