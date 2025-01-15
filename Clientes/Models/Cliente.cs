using System.ComponentModel.DataAnnotations;

namespace Clientes.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public byte[] Logotipo { get; set; }

        public List<Logradouro> Logradouros { get; set; }
    }
}
