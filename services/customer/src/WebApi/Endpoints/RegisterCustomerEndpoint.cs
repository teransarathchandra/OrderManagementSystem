using Application.DTOs;
using Application.Handlers;
using FluentValidation;
using MediatR;

namespace WebApi.Endpoints
{
    public static class RegisterCustomerEndpoint
    {
        public static void MapRegisterCustomerEndpoint(this WebApplication app)
        {
            app.MapPost("/customers/register", async (IMediator mediator, CreateCustomerDto dto, IValidator<CreateCustomerDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(dto);

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var result = await mediator.Send(new CreateCustomerCommand(dto));
                return Results.Ok(result);
            });
        }
    }
}
