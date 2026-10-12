namespace MinhaPrimeiraApi.Services;
using MinhaPrimeiraApi.Models;
using MinhaPrimeiraApi.Repositories;

public class ProdutoService : IProdutoService
{
	private readonly IProdutoRepository _repository;

	public ProdutoService(IProdutoRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<Produto>> ListarTodosAsync()
	{
		return await _repository.ListarTodosAsync();
	}

	public async Task<Produto?> BuscarPorIdAsync(int id)
	{
		return await _repository.BuscarPorIdAsync(id);
	}

	public async Task<Produto> CriarAsync(Produto produto)
	{
		if (produto.Preco < 0.01m)
		{
			throw new ArgumentException("O preço do produto deve ser maior que zero.");
		}
		if (await _repository.ExisteNomeDuplicado(produto.Nome))
		{
			throw new ArgumentException("Já existe um produto com o mesmo nome.");
		}
		return await _repository.CriarAsync(produto);
	}

	public async Task<Produto?> AtualizarAsync(int id, Produto produto)
	{
		return await _repository.AtualizarAsync(id, produto);
	}

	public async Task<bool> DeletarAsync(int id)
	{
		return await _repository.DeletarAsync(id);
	}
}