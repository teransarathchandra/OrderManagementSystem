using Domain.Models;
using MediatR;

namespace Application.Queries.RetrieveCustomer
{
    public class RetrieveCustomerQuery : IRequest<Customer>
    {
        public int CustomerId { get; }

        public RetrieveCustomerQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}