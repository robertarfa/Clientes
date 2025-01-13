namespace Clientes.Models.DTO
{
    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<LogradouroResponseDTO> Logradouros { get; set; }
    }
}
