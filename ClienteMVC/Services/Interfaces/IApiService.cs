using ClienteMVC.Models;
using ClienteMVC.Models.ViewModels;

public interface IApiService
{
    Task<string> Login(LoginViewModel model);
    Task<IEnumerable<Cliente>> GetClientesAsync();
    Task<Cliente> GetCliente(int id);
    Task<bool> CreateCliente(Cliente model);
    Task<bool> UpdateCliente(int id, ClienteUpdateViewModel model);
    Task<bool> DeleteCliente(int id);
}