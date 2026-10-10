using Microsoft.AspNetCore.Mvc;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Data;
using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraApi.Services;

namespace MinhaPrimeiraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
	private readonly IProdutoService _service;

	public ProdutosController(IProdutoService service)
	{
		_service = service;
	}

    [HttpGet]   
    public async Task<IActionResult> ListarTodos()
    {
		var produtos = await _service.ListarTodosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]   
    public async Task<IActionResult> BuscarPorId(int id)
    {
        if(id <= 0)
            return BadRequest("O ID deve ser maior que zero.");

        var produto = await _service.BuscarPorIdAsync(id);

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
            return BadRequest(ModelState);

		try
		{
			var produtoCriado = await _service.CriarAsync(prod);
			return CreatedAtAction(nameof(BuscarPorId), new { id = produtoCriado.Id }, produtoCriado);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message); // 400 
		}
    }

	[HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Produto prod)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var produto = await _service.AtualizarAsync(id, prod);

        if (produto == null)
            return NotFound($"Produto com ID {id} não encontrado.");
       
        return Ok($"Produto ID '{id}' atualizado com sucesso!");
    }

	[HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var removido = await _service.DeletarAsync(id);

        if (!removido)
        {
            return NotFound($"Produto com ID {id} não encontrado.");
        }

        return NoContent(); // 204 No Content
    }
}