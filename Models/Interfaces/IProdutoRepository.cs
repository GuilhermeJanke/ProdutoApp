using ProdutoAPI.Models.Entities;

namespace ProdutoAPI.Models.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task AddAsync(Produto produto);
        Task <bool>UpdateAsync(Produto produto);
        Task <bool>DeleteAsync(int id);
    }
}
