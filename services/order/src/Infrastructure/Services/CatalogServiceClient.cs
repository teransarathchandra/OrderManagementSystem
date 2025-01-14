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
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> ReduceProductQuantityAsync(int productId, int quantity)
        {
            var response = await _httpClient.PostAsync($"/catalog/products/{productId}/reduce?quantity={quantity}", null);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Product with ID {productId} not found.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new InvalidOperationException($"Unable to reduce quantity for Product {productId}.");
                }
                else
                {
                    throw new InvalidOperationException($"An unexpected error occurred while reducing quantity for Product {productId}: {response.StatusCode}");
                }
            }

            return true;
        }
    }
}