using Domain.Models;
using MediatR;

namespace Application.Catalog.Retrieve.GetCategoryById
{
    public class GetCategoryByIdQuery(Guid categoryId) : IRequest<Category>
    {
        public Guid CategoryId { get; } = categoryId;
    }
}
