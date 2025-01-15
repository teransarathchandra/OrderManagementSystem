using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class OrderItemValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            //RuleFor(x => x.Price)
            //    .GreaterThanOrEqualTo(0)
            //    .WithMessage("Price must be greater than or equal to 0.");
        }
    }
}