using System.Text.Json.Serialization;

namespace Clientes.Models
{
    public class Logradouro
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Endereco { get; set; }
        [JsonIgnore]
        public int? ClienteId { get; set; }
        [JsonIgnore]
        public Cliente? Cliente { get; set; }

    }
}
