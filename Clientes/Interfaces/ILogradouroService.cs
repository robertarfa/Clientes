using Clientes.Models;

public interface ILogradouroService
{
    Task<Logradouro> CriarLogradouro(Logradouro logradouro);
    Task<Logradouro> AtualizarLogradouro(int id, Logradouro logradouro);
    Task<Logradouro> ObterLogradouro(int id);
    Task<bool> RemoverLogradouro(int id);
    Task<IEnumerable<Logradouro>> ListarLogradourosPorCliente(int clienteId);
}