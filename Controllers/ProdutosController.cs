using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MinhaPrimeiraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

	public ProdutosController(AppDbContext context)
	{
		_context = context;
	}

    [HttpGet]   
    public async Task<IActionResult> ListarTodos()
    {
		var produtos = await _context.Produtos.ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]   
    public async Task<IActionResult> BuscarPorId(int id)
    {
        if(id <= 0)
        {
            return BadRequest("O ID deve ser maior que zero.");
        }

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

         if (produto == null)
        {
            return NotFound($"Produto com ID {id} não encontrado.");
        }

        return Ok(produto);
    }

    [HttpPost] 
    public async Task<IActionResult> Criar([FromBody] Produto prod)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Produtos.Add(prod);
        await _context.SaveChangesAsync();
        return Ok($"Produto '{prod.Nome}', '{prod.Preco}' criado com sucesso!");
    }

	[HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Produto prod)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var produtoExistente = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if (produtoExistente == null)
        {
            return NotFound($"Produto com ID {id} não encontrado.");
        }

        produtoExistente.Nome = prod.Nome;
        produtoExistente.Preco = prod.Preco;

        await _context.SaveChangesAsync();
        return Ok($"Produto '{produtoExistente.Nome}', '{produtoExistente.Preco}' atualizado com sucesso!");
    }

	[HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
        {
            return NotFound($"Produto com ID {id} não encontrado.");
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok($"Produto '{produto.Nome}', '{produto.Preco}' excluído com sucesso!");
    }
}