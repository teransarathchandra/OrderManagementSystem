using Domain.Models;
using MediatR;

namespace Application.Queries.SearchProducts
{
    public class SearchProductsQuery : IRequest<List<Product>>
    {
        public string Query { get; }

        public SearchProductsQuery(string query)
        {
            Query = query;
        }
    }
}