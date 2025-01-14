using Application.DTOs;
using Domain.Models;
using MediatR;

namespace Application.Commands.updateCustomer
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public int CustomerId { get; set; }
        public UpdateCustomerDto CustomerDto { get; set; }

        public UpdateCustomerCommand(int customerId, UpdateCustomerDto customerDto)
        {
            CustomerId = customerId;
            CustomerDto = customerDto;
        }
    }
}