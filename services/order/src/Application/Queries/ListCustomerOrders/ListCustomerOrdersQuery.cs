using Domain.Models;
using MediatR;

namespace Application.Queries.ListCustomerOrders
{
    public class ListCustomerOrdersQuery : IRequest<List<Order>>
    {
        public Guid CustomerId { get; }

        public ListCustomerOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}