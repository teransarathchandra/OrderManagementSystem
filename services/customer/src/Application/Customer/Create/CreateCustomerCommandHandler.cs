using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Customer.Create
{
    public class CreateCustomerCommandHandler(CustomerDbContext dbContext)
        : IRequestHandler<CreateCustomerCommand, Domain.Models.Customer>
    {
        public async Task<Domain.Models.Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Models.Customer
            {
                Name = request.CustomerDto.Name,
                Email = request.CustomerDto.Email,
                Address = request.CustomerDto.Address
            };

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }
    }
}
