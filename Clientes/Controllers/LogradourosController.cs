using Clientes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LogradourosController : ControllerBase
    {
        private readonly ILogradouroService _logradouroService;

        public LogradourosController(ILogradouroService logradouroService)
        {
            _logradouroService = logradouroService;
        }

        // Implemente os endpoints...
    }

}
