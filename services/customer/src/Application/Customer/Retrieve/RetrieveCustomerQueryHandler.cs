using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Customer.Retrieve
{
    public class RetrieveCustomerQueryHandler : IRequestHandler<RetrieveCustomerQuery, Domain.Models.Customer>
    {
        private readonly CustomerDbContext _dbContext;

        public RetrieveCustomerQueryHandler(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Models.Customer?> Handle(RetrieveCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);
        }
    }
}
