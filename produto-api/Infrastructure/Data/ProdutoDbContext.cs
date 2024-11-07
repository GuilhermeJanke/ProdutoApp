using ProdutoAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProdutoAPI.Infrastructure.Data
{
    public class ProdutoDbContext : DbContext
    {
        public ProdutoDbContext(DbContextOptions<ProdutoDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(p => p.DataCadastro)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

