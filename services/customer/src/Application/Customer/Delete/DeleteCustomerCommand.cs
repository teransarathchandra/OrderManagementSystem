using MediatR;

namespace Application.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand(Guid customerId) : IRequest<bool>
    {
        public Guid CustomerId { get; } = customerId;
    }
}
