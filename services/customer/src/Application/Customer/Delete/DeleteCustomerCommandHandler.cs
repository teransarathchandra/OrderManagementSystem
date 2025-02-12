using Infrastructure.Persistence;
using MediatR;

namespace Application.Customer.Delete
{
    public class DeleteCustomerCommandHandler(CustomerDbContext dbContext)
        : IRequestHandler<DeleteCustomerCommand, bool>
    {
        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await dbContext.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);
            if (customer == null)
            {
                return false;
            }

            dbContext.Customers.Remove(customer);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
