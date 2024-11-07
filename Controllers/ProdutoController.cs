using ProdutoAPI.Models.Entities;
using ProdutoAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProdutoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            var produtos = await _produtoService.GetAllAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Produto produto)
        {
            await _produtoService.AddAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            var success = await _produtoService.UpdateAsync(produto);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _produtoService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}