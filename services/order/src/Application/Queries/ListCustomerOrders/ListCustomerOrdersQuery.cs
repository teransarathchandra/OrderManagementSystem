using Domain.Models;
using MediatR;

namespace Application.Queries.ListCustomerOrders
{
    public class ListCustomerOrdersQuery : IRequest<List<Order>>
    {
        public int CustomerId { get; }

        public ListCustomerOrdersQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}