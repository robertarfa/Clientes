using ClienteMVC.Models;
using ClienteMVC.Models.ViewModels;

namespace ClienteMVC.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
        }

        public async Task<bool> CreateCliente(Cliente model)
        {

            var formData = new MultipartFormDataContent();

            // Adiciona os campos simples
            formData.Add(new StringContent(model.Nome), "Nome");
            formData.Add(new StringContent(model.Email), "Email");
            formData.Add(new StringContent(model.Senha), "Senha");

            // Adiciona os logradouros com índices
            if (model.Logradouros != null && model.Logradouros.Any())
            {
                for (int i = 0; i < model.Logradouros.Count; i++)
                {
                    formData.Add(new StringContent(model.Logradouros[i].Endereco), $"Logradouros[{i}].Endereco");
                }
            }

            // Adiciona o arquivo (logotipo), se existir
            if (model.Logotipo != null)
            {
                var fileContent = new StreamContent(model.Logotipo.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Logotipo.ContentType);
                formData.Add(fileContent, "logotipo", model.Logotipo.FileName);
            }

            // Envia a requisição para a API
            var response = await _httpClient.PostAsync("api/clientes", formData);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro: {response.StatusCode}");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCliente(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<Cliente> GetCliente(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
            var cliente = await response.Content.ReadFromJsonAsync<Cliente>();
            return cliente ?? new Cliente();
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            var response = await _httpClient.GetAsync("api/clientes");
            response.EnsureSuccessStatusCode();
            var clientes = await response.Content.ReadFromJsonAsync<IEnumerable<Cliente>>();
            return clientes ?? new List<Cliente>();
        }

        public async Task<string> Login(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Clientes/login", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                return result.Token;
            }
            return null;
        }

        public async Task<bool> UpdateCliente(int id, ClienteViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/clientes/{id}", model);
            response.EnsureSuccessStatusCode();
            var updatedCliente = await response.Content.ReadFromJsonAsync<Cliente>();
            return response.IsSuccessStatusCode;
        }
    }
}
