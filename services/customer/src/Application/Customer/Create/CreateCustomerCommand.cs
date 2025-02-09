using Domain.Models;
using MediatR;

namespace Application.Customer.Create
{
    public class CreateCustomerCommand : IRequest<Domain.Models.Customer>
    {
        public CreateCustomerDto CustomerDto { get; set; }

        public CreateCustomerCommand(CreateCustomerDto customerDto)
        {
            CustomerDto = customerDto;
        }
    }
}
