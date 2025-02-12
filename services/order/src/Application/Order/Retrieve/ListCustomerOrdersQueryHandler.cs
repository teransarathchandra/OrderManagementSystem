using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Order.Retrieve
{
    public class ListCustomerOrdersQueryHandler(OrderDbContext dbContext)
        : IRequestHandler<ListCustomerOrdersQuery, List<Domain.Models.Order>>
    {
        public async Task<List<Domain.Models.Order>> Handle(ListCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Orders
                .Include(o => o.Items)
                .Where(o => o.CustomerId == request.CustomerId)
                .ToListAsync(cancellationToken);
        }
    }
}
