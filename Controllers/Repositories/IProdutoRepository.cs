namespace MinhaPrimeiraApi.Repositories;

using MinhaPrimeiraApi.Models;

public interface IProdutoRepository
{
	Task<List<Produto>> ListarTodosAsync();
	Task<Produto?> BuscarPorIdAsync(int id);
	Task<Produto> CriarAsync(Produto produto);
	Task<Produto?> AtualizarAsync(int id, Produto produto);
	Task<bool> DeletarAsync(int id);
	Task<bool> ExisteNomeDuplicado(string nome);
}
