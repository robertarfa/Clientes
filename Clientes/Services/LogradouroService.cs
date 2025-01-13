using Clientes.Data;
using Clientes.Models;
using Clientes.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class LogradouroService : ILogradouroService
{
    private readonly ApplicationDbContext _context;

    public LogradouroService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Logradouro> AtualizarLogradouro(int id, Logradouro logradouro)
    {
        // Verifica se existe outro cliente com o mesmo email
        var logradouroExistente = await _context.Logradouros
            .FirstOrDefaultAsync(c => c.Id != logradouro.Id);

        if (logradouroExistente != null)
        {
            throw new Exception("Já existe um cliente cadastrado com este email");
        }

        _context.Entry(logradouro).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return logradouro;

    }

    public async Task<Logradouro> CriarLogradouro(Logradouro logradouro)
    {
        _context.Logradouros.Add(logradouro);
        await _context.SaveChangesAsync();

        return logradouro;
    }

    public async Task<IEnumerable<Logradouro>> ListarLogradouros()
    {
        return await _context.Logradouros.ToListAsync();
    }

    public async Task<Logradouro?> ObterLogradouro(int id)
    {
        var logradouro = await _context.Logradouros.FirstOrDefaultAsync(c => c.Id == id);

        return logradouro;
    }

    public async Task<bool> RemoverLogradouro(int id)
    {
        var logradouro = await _context.Logradouros.FindAsync(id);
        if (logradouro == null)
        {
            return false;
        }

        _context.Logradouros.Remove(logradouro);
        await _context.SaveChangesAsync();

        return true;
    }

}