using System.ComponentModel.DataAnnotations;

namespace Clientes.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }

        // Relacionamento com Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
