﻿using Application.Queries.GetOrder;
using MediatR;

namespace OrderWebApi.Endpoints
{
    public static class GetOrderEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/orders/{orderId:int}", async (IMediator mediator, int orderId) =>
            {
                var order = await mediator.Send(new GetOrderQuery(orderId));
                if (order == null)
                {
                    return Results.NotFound($"Order with ID {orderId} not found.");
                }

                return Results.Ok(order);
            });
        }
    }
}