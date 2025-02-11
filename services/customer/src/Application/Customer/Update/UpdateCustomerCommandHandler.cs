using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Customer.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Domain.Models.Customer>
    {
        private readonly CustomerDbContext _dbContext;

        public UpdateCustomerCommandHandler(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Models.Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
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
