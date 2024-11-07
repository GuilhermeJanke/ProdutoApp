using ProdutoAPI.Models.Entities;
using ProdutoAPI.Models.Interfaces;

namespace ProdutoAPI.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Produto produto)
        {
            await _produtoRepository.AddAsync(produto);
        }

        // Método para atualizar um produto existente
        public async Task<bool> UpdateAsync(Produto produto)
        {
            return await _produtoRepository.UpdateAsync(produto);
        }

        // Método para excluir um produto
        public async Task<bool> DeleteAsync(int id)
        {
            return await _produtoRepository.DeleteAsync(id);
        }
    }
}