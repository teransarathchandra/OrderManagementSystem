using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly CustomerDbContext _dbContext;

        public UpdateCustomerCommandHandler(CustomerDbContext dbContext)
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