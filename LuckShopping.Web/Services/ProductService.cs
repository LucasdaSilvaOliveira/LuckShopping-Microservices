using LuckShopping.Web.Models;
using LuckShopping.Web.Services.IServices;
using System.Text.Json;

namespace LuckShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/Product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _client.GetAsync(BasePath);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var products = JsonSerializer.Deserialize<List<ProductModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            return products;
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var product = JsonSerializer.Deserialize<ProductModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            return product;
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _client.PostAsJsonAsync(BasePath, model);
            if (response.IsSuccessStatusCode)
            {
                return model;
            }
            else
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Something went wrong when calling API: {response.StatusCode} - {responseMessage}");
            }
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PutAsJsonAsync(BasePath, model);
            if (response.IsSuccessStatusCode)
            {
                var product = await this.FindProductById(model.Id);

                if (product == null) throw new ApplicationException("Produto atualizado, mas houve um erro ao retorna-lo do banco!");

                return product;
            }
            else
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Something went wrong when calling API: {response.StatusCode} - {responseMessage}");
            }
        }

        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            } else
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Something went wrong when calling API: {response.StatusCode} - {responseMessage}");
            }
        }
    }
}
