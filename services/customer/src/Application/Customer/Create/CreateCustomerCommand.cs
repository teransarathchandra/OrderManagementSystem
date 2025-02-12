using MediatR;

namespace Application.Customer.Create
{
    public class CreateCustomerCommand(CreateCustomerDto customerDto) : IRequest<Domain.Models.Customer>
    {
        public CreateCustomerDto CustomerDto { get; set; } = customerDto;
    }
}
