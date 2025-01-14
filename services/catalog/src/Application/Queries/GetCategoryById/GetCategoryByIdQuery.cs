using Domain.Models;
using MediatR;

namespace Application.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int CategoryId { get; }

        public GetCategoryByIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}