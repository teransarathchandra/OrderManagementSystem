using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public CreateCustomerDto CustomerDto { get; set; }

        public CreateCustomerCommand(CreateCustomerDto customerDto)
        {
            CustomerDto = customerDto;
        }
    }
}