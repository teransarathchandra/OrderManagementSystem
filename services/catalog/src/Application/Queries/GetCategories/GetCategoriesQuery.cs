using Domain.Models;
using MediatR;

namespace Application.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<Category>>
    {
    }
}