using FluentValidation;

namespace Application.Order.Create
{
    internal sealed class OrderItemValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId must be a valid GUID");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            //RuleFor(x => x.Price)
            //    .GreaterThanOrEqualTo(0)
            //    .WithMessage("Price must be greater than or equal to 0.");
        }
    }
}