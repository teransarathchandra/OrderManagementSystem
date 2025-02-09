using Domain.Models;
using MediatR;

namespace Application.Customer.Update
{
    public class UpdateCustomerCommand : IRequest<Domain.Models.Customer>
    {
        public Guid CustomerId { get; set; }
        public UpdateCustomerDto CustomerDto { get; set; }

        public UpdateCustomerCommand(Guid customerId, UpdateCustomerDto customerDto)
        {
            CustomerId = customerId;
            CustomerDto = customerDto;
        }
    }
}
