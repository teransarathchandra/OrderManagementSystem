﻿using FluentValidation;

namespace Application.Customer.Create
{
    internal sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(250).WithMessage("Address must be less than 250 characters");
        }
    }
}