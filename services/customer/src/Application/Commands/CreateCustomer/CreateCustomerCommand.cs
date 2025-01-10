using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
