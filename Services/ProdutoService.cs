using ProdutoAppMVC.Models;

namespace ProdutoAppMVC.Services
{
    public class ProdutoService
    {
        private readonly HttpClient _httpClient;

        public ProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Produto>>("produto");
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Produto>($"produto/{id}");
        }

        public async Task CreateAsync(Produto produto)
        {
            await _httpClient.PostAsJsonAsync("produto", produto);
        }

        public async Task UpdateAsync(Produto produto)
        {
            await _httpClient.PutAsJsonAsync($"produto/{produto.Id}", produto);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"produto/{id}");
        }
    }
}
