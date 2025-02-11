using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Order.Retrieve
{
    public class ListCustomerOrdersQueryHandler : IRequestHandler<ListCustomerOrdersQuery, List<Domain.Models.Order>>
    {
        private readonly OrderDbContext _dbContext;

        public ListCustomerOrdersQueryHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Domain.Models.Order>> Handle(ListCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                .Where(o => o.CustomerId == request.CustomerId)
                .ToListAsync(cancellationToken);
        }
    }
}
