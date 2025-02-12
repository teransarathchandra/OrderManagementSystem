using Domain.Models;
using MediatR;

namespace Application.Customer.Retrieve
{
    public class RetrieveCustomerQuery(Guid customerId) : IRequest<Domain.Models.Customer>
    {
        public Guid CustomerId { get; } = customerId;
    }
}
