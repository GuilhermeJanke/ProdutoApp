using ProdutoAPI.Models.Entities;
using ProdutoAPI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProdutoAPI.Infrastructure.Data;

namespace ProdutoAPI.Infraestructure.Repositories
{

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDbContext _context;

        public ProdutoRepository(ProdutoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task AddAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Produto produto)
        {
            try
            {
                var existingProduto = await _context.Produtos.FindAsync(produto.Id);
                
                if (existingProduto == null)
                {
                    return false;
                }

                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                //n vou fazer tratamento de exception ainda, normalmente eu colocaria um _logger...
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    return false;
                }

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                //n vou fazer tratamento de exception ainda, normalmente eu colocaria um _logger...
                return false;
            }
        }
    }
}
