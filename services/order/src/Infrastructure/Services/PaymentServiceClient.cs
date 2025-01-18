using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class PaymentServiceClient
    {
        private readonly HttpClient _httpClient;

        public PaymentServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ProcessPaymentAsync(Guid orderId, decimal amount)
        {
            var response = await _httpClient.PostAsJsonAsync($"/payments/process", new { OrderId = orderId, Amount = amount });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}