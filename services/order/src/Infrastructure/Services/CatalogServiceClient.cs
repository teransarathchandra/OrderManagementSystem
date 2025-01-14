using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class CatalogServiceClient
    {
        private readonly HttpClient _httpClient;

        public CatalogServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CheckProductAvailabilityAsync(int productId, int quantity)
        {
            var response = await _httpClient.GetAsync($"/catalog/products/{productId}/availability?quantity={quantity}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task ReduceProductQuantityAsync(int productId, int quantity)
        {
            var response = await _httpClient.PostAsJsonAsync($"/catalog/products/{productId}/reduce", new { Quantity = quantity });
            response.EnsureSuccessStatusCode();
        }
    }
}