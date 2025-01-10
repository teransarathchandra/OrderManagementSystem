using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.DeleteCustomer
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly CustomerDbContext _dbContext;

        public DeleteCustomerHandler(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);
            if (customer == null)
            {
                return false;
            }

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
