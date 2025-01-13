using Clientes.Models;
using Clientes.Models.DTO;
using Microsoft.AspNetCore.Mvc;

public interface IClienteService
{
    Task<Cliente> CriarCliente(Cliente cliente, string senha);
    Task<Cliente> AtualizarCliente(Cliente cliente);
    Task<Cliente?> ObterCliente(int id);
    Task<bool> RemoverCliente(int id);
    Task<ActionResult<IEnumerable<ClienteResponseDTO>>> ListarClientes();
    Task<Usuario> AutenticarCliente(string email, string senha);
}