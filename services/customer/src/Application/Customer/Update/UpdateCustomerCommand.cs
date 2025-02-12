using Domain.Models;
using MediatR;

namespace Application.Customer.Update
{
    public class UpdateCustomerCommand(Guid customerId, UpdateCustomerDto customerDto)
        : IRequest<Domain.Models.Customer>
    {
        public Guid CustomerId { get; set; } = customerId;
        public UpdateCustomerDto CustomerDto { get; set; } = customerDto;
    }
}
