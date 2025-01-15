using Clientes.Models;
using Clientes.Models.DTO;

public interface ILogradouroService
{
    Task<LogradouroCreateDTO> CriarLogradouro(LogradouroCreateDTO logradouro);
    Task<Logradouro> AtualizarLogradouro(int id, Logradouro logradouro);
    Task<Logradouro?> ObterLogradouro(int id);
    Task<bool> RemoverLogradouro(int id);
    Task<IEnumerable<Logradouro>> ListarLogradouros();
}