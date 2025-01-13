using Clientes.Models;
using Clientes.Models.DTO;
using Clientes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly TokenService _tokenService;

        public ClientesController(IClienteService clienteService, TokenService tokenService)
        {
            _clienteService = clienteService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var usuario = await _clienteService.AutenticarCliente(model.Email, model.Senha);

            if (usuario == null)
                return Unauthorized();

            var token = _tokenService.GerarToken(usuario.Cliente); // Gera o token usando as informações do cliente
            return Ok(new { token });
        }

     

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromForm] ClienteCreateDTO clienteDTO, IFormFile logotipo)
        {
            var cliente = new Cliente
            {
                Nome = clienteDTO.Nome,
                Email = clienteDTO.Email,
                Logradouros = new List<Logradouro>
        {
            new Logradouro
            {
                Endereco = clienteDTO.Endereco
            }
        }
            };

            if (logotipo != null)
            {
                using (var ms = new MemoryStream())
                {
                    await logotipo.CopyToAsync(ms);
                    cliente.Logotipo = ms.ToArray();
                }
            }

            try
            {
                var novoCliente = await _clienteService.CriarCliente(cliente, clienteDTO.Senha);
              
                return Ok(novoCliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDTO>>> GetClientes()
        {
            return await _clienteService.ListarClientes();
        }
    }



}
