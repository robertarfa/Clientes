using System.ComponentModel.DataAnnotations;

namespace ClienteMVC.Models.ViewModels
{
    public class ClienteViewModel
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }
        public string LogotipoBase64 { get; set; }
        public List<Logradouro> Logradouros { get; set; }
    }
}
