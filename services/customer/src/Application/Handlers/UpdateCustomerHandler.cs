using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Handlers
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

    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly CustomerDbContext _dbContext;

        public UpdateCustomerHandler(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FindAsync(request.CustomerId);
            if (customer == null) return null;

            customer.Name = request.CustomerDto.Name;
            customer.Email = request.CustomerDto.Email;
            customer.Address = request.CustomerDto.Address;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }
    }
}
