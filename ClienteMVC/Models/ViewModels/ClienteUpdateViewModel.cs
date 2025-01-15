namespace ClienteMVC.Models
{
    public class ClienteUpdateViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public IFormFile Logotipo { get; set; }
        public List<Logradouro> Logradouros { get; set; }
    }
}
