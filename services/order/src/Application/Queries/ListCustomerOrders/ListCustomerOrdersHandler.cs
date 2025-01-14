using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.ListCustomerOrders
{
    public class ListCustomerOrdersHandler : IRequestHandler<ListCustomerOrdersQuery, List<Order>>
    {
        private readonly OrderDbContext _dbContext;

        public ListCustomerOrdersHandler(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> Handle(ListCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(o => o.Items)
                .Where(o => o.CustomerId == request.CustomerId)
                .ToListAsync(cancellationToken);
        }
    }
}