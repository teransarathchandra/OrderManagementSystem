﻿using Application.Order.Update;
using Domain.Models;
using MediatR;

namespace OrderWebApi.Endpoints
{
    internal static class UpdateOrderStatusEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPut("/orders/{orderId}/status", async (IMediator mediator, Guid orderId, OrderStatus status) =>
            {
                var result = await mediator.Send(new UpdateOrderStatusCommand(orderId, status));
                return result ? Results.Ok($"Order status updated to '{status}'.") : Results.NotFound($"Order with ID {orderId} not found.");
            });
        }
    }
}
