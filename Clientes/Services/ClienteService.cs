using Clientes.Data;
using Clientes.Models;
using Clientes.Models.DTO;
using Clientes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ClienteService : IClienteService
{
    private readonly ApplicationDbContext _context;

    public ClienteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> AutenticarCliente(string email, string senha)
    {
        var usuario = await _context.Usuarios.Include(u => u.Cliente)
                          .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario == null || !VerificarSenha(senha, usuario.SenhaHash))
        {
            return null; // Autenticação falhou
        }

        return usuario; // Retorna o usuário autenticado
    }

    private bool VerificarSenha(string senha, string senhaHash)
    {
    
        return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
    }

    public async Task<Cliente> CriarCliente(Cliente cliente, string senha)
    {
        // Verifica se já existe um cliente com o mesmo e-mail
        if (await _context.Clientes.AnyAsync(c => c.Email == cliente.Email))
        {
            throw new Exception("Um cliente com este e-mail já está registrado.");
        }

        // Adiciona o cliente ao contexto
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Cria um novo usuário associado ao cliente
        var usuario = new Usuario
        {
            Email = cliente.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
            ClienteId = cliente.Id // Associa o usuário ao cliente
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return cliente;
    }

    public async Task<ActionResult<IEnumerable<ClienteResponseDTO>>> ListarClientes()
    {
        return await _context.Clientes
        .Include(c => c.Logradouros)
        .Select(c => new ClienteResponseDTO
        {
            Id = c.Id,
            Nome = c.Nome,
            Email = c.Email,
            Logradouros = c.Logradouros.Select(l => new LogradouroResponseDTO
            {
                Id = l.Id,
                Endereco = l.Endereco
            }).ToList()
        })
        .ToListAsync();
    }

    public Task<Cliente> AtualizarCliente(int id, Cliente cliente)
    {
        throw new NotImplementedException();
    }
    public Task<Cliente> ObterCliente(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoverCliente(int id)
    {
        throw new NotImplementedException();
    }

   
}
