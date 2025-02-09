using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Queries.RetrieveCustomer
{
    public class RetrieveCustomerQueryHandler : IRequestHandler<RetrieveCustomerQuery, Customer>
    {
        private readonly CustomerDbContext _dbContext;

        public RetrieveCustomerQueryHandler(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> Handle(RetrieveCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);
        }
    }
}