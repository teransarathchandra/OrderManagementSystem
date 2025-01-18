using Domain.Models;
using MediatR;

namespace Application.Queries.RetrieveCustomer
{
    public class RetrieveCustomerQuery : IRequest<Customer>
    {
        public Guid CustomerId { get; }

        public RetrieveCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}