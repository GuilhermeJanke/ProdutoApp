using Moq;
using ProdutoAPI.Application.Services;
using ProdutoAPI.Models.Entities;
using ProdutoAPI.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ProdutoServiceTests
{
    private readonly Mock<IProdutoRepository> _mockRepository;
    private readonly ProdutoService _produtoService;

    public ProdutoServiceTests()
    {
        _mockRepository = new Mock<IProdutoRepository>();
        _produtoService = new ProdutoService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarListaDeProdutos()
    {
        var produtos = new List<Produto>
        {
            new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m },
            new Produto { Id = 2, Nome = "Produto 2", Preco = 20.0m }
            //teste
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produtos);

        var result = await _produtoService.GetAllAsync();

        var produtoList = result.ToList();
        Assert.Equal(2, produtoList.Count);
        Assert.Equal("Produto 1", produtoList[0].Nome);
        Assert.Equal("Produto 2", produtoList[1].Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ProdutoExistente_DeveRetornarProduto()
    {
        var produto = new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m };
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

        var result = await _produtoService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Produto 1", result.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ProdutoNaoExistente_DeveRetornarNull()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Produto)null);
        var result = await _produtoService.GetByIdAsync(999);
        Assert.Null(result); 
    }

    [Fact]
    public async Task AddAsync_ProdutoValido_DeveAdicionarProduto()
    {
        var produto = new Produto { Id = 1, Nome = "Produto Novo", Preco = 10.0m };
        await _produtoService.AddAsync(produto);
        _mockRepository.Verify(repo => repo.AddAsync(produto), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ProdutoExistente_DeveRetornarTrue()
    {
        var produtoExistente = new Produto { Id = 1, Nome = "Produto 1", Preco = 10 };

        _mockRepository.Setup(repo => repo.GetByIdAsync(produtoExistente.Id))
                              .ReturnsAsync(produtoExistente);

        var resultado = await _produtoService.UpdateAsync(produtoExistente);

        Assert.True(resultado);
    }

    [Fact]
    public async Task UpdateAsync_ProdutoNaoExistente_DeveRetornarFalse()
    {
        var produto = new Produto { Id = 999, Nome = "Produto Teste", Preco = 10.0m };
        _mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Produto)null);

        var result = await _produtoService.UpdateAsync(produto);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ProdutoExistente_DeveRetornarTrue()
    {
        var produto = new Produto { Id = 1, Nome = "Produto Teste", Preco = 10.0m };
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

        var result = await _produtoService.DeleteAsync(1);

        Assert.True(result);
        _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ProdutoNaoExistente_DeveRetornarFalse()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Produto)null);

        var result = await _produtoService.DeleteAsync(999);

        Assert.False(result); 
    }
}