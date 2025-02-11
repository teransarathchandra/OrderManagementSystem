using Domain.Models;
using MediatR;

namespace Application.Catalog.Retrieve.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<Category>>
    {
    }
}