using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Customer.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Domain.Models.Customer>
    {
        private readonly CustomerDbContext _dbContext;

        public CreateCustomerCommandHandler(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Models.Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Models.Customer
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
