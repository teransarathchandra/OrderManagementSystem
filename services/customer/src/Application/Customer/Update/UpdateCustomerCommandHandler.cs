using Infrastructure.Persistence;
using MediatR;

namespace Application.Customer.Update
{
    public class UpdateCustomerCommandHandler(CustomerDbContext dbContext)
        : IRequestHandler<UpdateCustomerCommand, Domain.Models.Customer>
    {
        public async Task<Domain.Models.Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await dbContext.Customers.FindAsync(request.CustomerId);
            if (customer == null) return null;

            customer.Name = request.CustomerDto.Name;
            customer.Email = request.CustomerDto.Email;
            customer.Address = request.CustomerDto.Address;

            await dbContext.SaveChangesAsync(cancellationToken);

            return customer;
        }
    }
}
