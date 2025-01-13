using Clientes.Data;
using Clientes.Models;

public class LogradouroService : ILogradouroService
{
    private readonly ApplicationDbContext _context;

    public LogradouroService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Logradouro> AtualizarLogradouro(int id, Logradouro logradouro)
    {
        throw new NotImplementedException();
    }

    public Task<Logradouro> CriarLogradouro(Logradouro logradouro)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Logradouro>> ListarLogradourosPorCliente(int clienteId)
    {
        throw new NotImplementedException();
    }

    public Task<Logradouro> ObterLogradouro(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoverLogradouro(int id)
    {
        throw new NotImplementedException();
    }

}