using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.CreateCustomer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly CustomerDbContext _dbContext;

        public CreateCustomerHandler(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.CustomerDto.Name,
                Email = request.CustomerDto.Email,
                Address = request.CustomerDto.Address
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }
    }
}
