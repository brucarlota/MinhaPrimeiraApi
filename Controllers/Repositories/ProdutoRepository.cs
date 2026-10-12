using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraApi.Data;
using MinhaPrimeiraApi.Models;

namespace MinhaPrimeiraApi.Repositories;

public class ProdutoRepository : IProdutoRepository
{
	private readonly AppDbContext _context;

	public ProdutoRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<List<Produto>> ListarTodosAsync()
	{
		return await _context.Produtos.ToListAsync();
	}

	public async Task<Produto?> BuscarPorIdAsync(int id)
	{
		return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task<Produto> CriarAsync(Produto produto)
	{
		_context.Produtos.Add(produto);
		await _context.SaveChangesAsync();
		return produto;
	}

	public async Task<Produto?> AtualizarAsync(int id, Produto produto)
	{
		var existingProduto = await _context.Produtos.FindAsync(id);
		if (existingProduto == null)
		{
			return null;
		}

		existingProduto.Nome = produto.Nome;
		existingProduto.Preco = produto.Preco;

		await _context.SaveChangesAsync();
		return existingProduto;
	}

	public async Task<bool> DeletarAsync(int id)
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

	public async Task<bool> ExisteNomeDuplicado(string nome)
	{
		return await _context.Produtos.AnyAsync(p => p.Nome == nome);
	}
}
