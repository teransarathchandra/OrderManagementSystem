using Domain.Models;
using MediatR;

namespace Application.Order.Retrieve
{
    public class ListCustomerOrdersQuery : IRequest<List<Domain.Models.Order>>
    {
        public Guid CustomerId { get; }

        public ListCustomerOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
