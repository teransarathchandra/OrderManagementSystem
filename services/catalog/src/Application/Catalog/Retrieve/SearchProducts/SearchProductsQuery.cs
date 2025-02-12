using Domain.Models;
using MediatR;

namespace Application.Catalog.Retrieve.SearchProducts
{
    public class SearchProductsQuery(string query) : IRequest<List<Product>>
    {
        public string Query { get; } = query;
    }
}
