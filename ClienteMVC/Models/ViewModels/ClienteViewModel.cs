using System.ComponentModel.DataAnnotations;

namespace ClienteMVC.Models.ViewModels
{
    public class ClienteViewModel
    {
   
        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Endereco { get; set; }

        public IFormFile Logotipo { get; set; }
    }
}
