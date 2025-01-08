using Application.DTOs;
using Application.Handlers;
using FluentValidation;
using MediatR;

namespace WebApi.Endpoints
{
    public static class UpdateCustomerEndpoint
    {
        public static void MapUpdateCustomerEndpoint(this WebApplication app)
        {
            app.MapPut("/customers/{customerId}", async (IMediator mediator, int customerId, UpdateCustomerDto dto, IValidator<UpdateCustomerDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var result = await mediator.Send(new UpdateCustomerCommand(customerId, dto));
                if (result == null)
                {
                    return Results.NotFound($"Customer with ID {customerId} not found.");
                }

                return Results.Ok(result);
            });
        }
    }
}
