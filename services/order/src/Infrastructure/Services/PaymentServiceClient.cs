﻿using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class PaymentServiceClient(HttpClient httpClient)
    {
        public async Task<bool> ProcessPaymentAsync(Guid orderId, decimal amount)
        {
            var response = await httpClient.PostAsJsonAsync($"/payments/process", new { OrderId = orderId, Amount = amount });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
