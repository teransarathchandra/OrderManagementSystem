using Domain.Models;
using MediatR;

namespace Application.Customer.Retrieve
{
    public class RetrieveCustomerQuery : IRequest<Domain.Models.Customer>
    {
        public Guid CustomerId { get; }

        public RetrieveCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
