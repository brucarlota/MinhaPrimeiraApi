namespace MinhaPrimeiraApi.Services;

using MinhaPrimeiraApi.Models;

public interface IProdutoService
{
	Task<List<Produto>> ListarTodosAsync();
	Task<Produto?> BuscarPorIdAsync(int id);
	Task<Produto> CriarAsync(Produto produto);
	Task<Produto?> AtualizarAsync(int id, Produto produto);
	Task<bool> DeletarAsync(int id);
}
