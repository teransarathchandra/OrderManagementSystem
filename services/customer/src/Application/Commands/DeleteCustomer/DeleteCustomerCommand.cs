using MediatR;

namespace Application.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int CustomerId { get; }

        public DeleteCustomerCommand(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
