using System.Text.Json.Serialization;

namespace Clientes.Models
{
    public class Logradouro
    {
        public int Id { get; set; }
        public string Endereco { get; set; }
        public int ClienteId { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }

    }
}
