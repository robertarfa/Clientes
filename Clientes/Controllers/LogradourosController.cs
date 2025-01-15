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
    public class LogradourosController : ControllerBase
    {
        private readonly ILogradouroService _logradouroService;
        private readonly TokenService _tokenService;


        public LogradourosController(ILogradouroService logradouroService, TokenService tokenService)
        {
            _logradouroService = logradouroService;
            _tokenService = tokenService;
        }

        // GET: api/Logradouros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logradouro>>> GetLogradouros()
        {
            var logradouros = await _logradouroService.ListarLogradouros();
            return Ok(logradouros);
        }

        // GET: api/Logradouroes/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Logradouro>> GetLogradouro(int id)
        {
            var logradouro = await _logradouroService.ObterLogradouro(id);

            if (logradouro == null)
            {
                return NotFound();
            }

            return logradouro;
        }

        // PUT: api/Logradouroes/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogradouro(int id, Logradouro logradouro)
        {

            var result = await _logradouroService.AtualizarLogradouro(id, logradouro);

         if(result != null)
            {
                return Ok(result);
            }

            return NoContent();
        }

        // POST: api/Logradouroes
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Logradouro>> PostLogradouro(LogradouroCreateDTO logradouro)
        {
            var result = await _logradouroService.CriarLogradouro(logradouro);
            return CreatedAtAction("GetLogradouro", new { id = result }, result);
        }

        // DELETE: api/Logradouroes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogradouro(int id)
        {
            var logradouro = await _logradouroService.RemoverLogradouro(id);

            if (!logradouro)
            {
                return NotFound();
            }

            return NoContent();
        }

    

    }

    internal record NewRecord(object Id);
}
