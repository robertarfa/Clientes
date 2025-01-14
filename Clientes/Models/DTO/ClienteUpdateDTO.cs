using System.ComponentModel.DataAnnotations;

namespace Clientes.Models.DTO
{
    public class ClienteUpdateDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        public string Endereco { get; set; }
    }
}
