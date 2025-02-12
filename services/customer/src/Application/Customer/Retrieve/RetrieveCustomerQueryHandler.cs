using Infrastructure.Persistence;
using MediatR;

namespace Application.Customer.Retrieve
{
    public class RetrieveCustomerQueryHandler(CustomerDbContext dbContext)
        : IRequestHandler<RetrieveCustomerQuery, Domain.Models.Customer>
    {
        public async Task<Domain.Models.Customer?> Handle(RetrieveCustomerQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);
        }
    }
}
