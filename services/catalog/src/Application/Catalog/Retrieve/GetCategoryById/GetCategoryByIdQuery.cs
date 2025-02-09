using Domain.Models;
using MediatR;

namespace Application.Catalog.Retrieve.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public Guid CategoryId { get; }

        public GetCategoryByIdQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}