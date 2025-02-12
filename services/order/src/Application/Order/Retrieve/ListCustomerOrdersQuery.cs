using Domain.Models;
using MediatR;

namespace Application.Order.Retrieve
{
    public class ListCustomerOrdersQuery(Guid customerId) : IRequest<List<Domain.Models.Order>>
    {
        public Guid CustomerId { get; } = customerId;
    }
}
