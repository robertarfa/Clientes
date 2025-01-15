using Clientes.Models;
using Clientes.Models.DTO;
using Clientes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                Logradouros = clienteDTO.Logradouros.Select(l => new Logradouro { Endereco = l }).ToList()
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

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDTO>>> GetClientes()
        {
            return await _clienteService.ListarClientes();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromForm] ClienteUpdateDTO clienteDTO, IFormFile logotipo)
        {
            var cliente = await _clienteService.ObterCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            // Atualiza os dados básicos
            cliente.Nome = clienteDTO.Nome;
            cliente.Email = clienteDTO.Email;

            // Atualiza o logotipo se fornecido
            if (logotipo != null)
            {
                using (var ms = new MemoryStream())
                {
                    await logotipo.CopyToAsync(ms);
                    cliente.Logotipo = ms.ToArray();
                }
            }

            // Atualiza o endereço
            if (!string.IsNullOrEmpty(clienteDTO.Endereco))
            {
                var logradouro = new Logradouro
                {
                    Endereco = clienteDTO.Endereco,
                    ClienteId = cliente.Id
                };
                cliente.Logradouros.Add(logradouro);
            }

            try
            {
                await _clienteService.AtualizarCliente(cliente);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Clientes/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.ObterCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteService.RemoverCliente(id);
            if (!cliente)
            {
                return NotFound();
            }

            return NoContent();
        }
    }


}
