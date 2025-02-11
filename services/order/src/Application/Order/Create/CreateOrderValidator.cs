using FluentValidation;

namespace Application.Order.Create
{
    internal sealed class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId must be a valid GUID.");

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());

            RuleFor(x => x.ShippingAddress)
                .NotEmpty()
                .WithMessage("Shipping address is required.");
        }
    }
}
