using System.Net.Http.Headers;
using System.Reflection;
using ClienteMVC.Models;
using ClienteMVC.Models.ViewModels;

namespace ClienteMVC.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateCliente(Cliente model)
        {

            var formData = FormatData(model);

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
            var teste = await response.Content.ReadAsStringAsync();
            var clienteDto = await response.Content.ReadFromJsonAsync<ClienteViewModel>();

            if (clienteDto == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            var logotipoFile = ConvertFromFile(clienteDto);

            var cliente = new Cliente
            {
                Id = id,
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                Logradouros = clienteDto.Logradouros.Select(l => new Logradouro { Endereco = l.Endereco }).ToList(),
                Logotipo = logotipoFile,
            };

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

        public async Task<bool> UpdateCliente(int id, ClienteUpdateViewModel model)
        {

            var formData = FormatData(model);

            var response = await _httpClient.PutAsJsonAsync($"api/clientes/{id}", formData);
            response.EnsureSuccessStatusCode();
            var updatedCliente = await response.Content.ReadFromJsonAsync<Cliente>();
            return response.IsSuccessStatusCode;


        }

        private IFormFile ConvertFromFile(ClienteViewModel clienteDto)
        {
            IFormFile logotipoFile = null;

            if (!string.IsNullOrEmpty(clienteDto.LogotipoBase64))
            {
                byte[] fileBytes = Convert.FromBase64String(clienteDto.LogotipoBase64);
                var stream = new MemoryStream(fileBytes);
                logotipoFile = new FormFile(stream, 0, fileBytes.Length, null, "logotipo")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/octet-stream"
                };
            }

            return logotipoFile;
        }

        private AuthenticationHeaderValue ReturnToken()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token JWT não encontrado na sessão.");
            }

            // Adiciona o token JWT ao cabeçalho Authorization
            return new AuthenticationHeaderValue("Bearer", token);
        }

        private MultipartFormDataContent FormatData(Cliente model)
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
                    formData.Add(new StringContent(model.Logradouros[i].Endereco), $"Logradouros[{i}]");
                }
            }

            // Adiciona o arquivo (logotipo), se existir
            if (model.Logotipo != null)
            {
                var fileContent = new StreamContent(model.Logotipo.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Logotipo.ContentType);
                formData.Add(fileContent, "logotipo", model.Logotipo.FileName);
            }

            return formData;
        }

        private MultipartFormDataContent FormatData(ClienteUpdateViewModel model)
        {
            var formData = new MultipartFormDataContent();

            // Adiciona os campos simples
            formData.Add(new StringContent(model.Nome), "Nome");
            formData.Add(new StringContent(model.Email), "Email");

            // Adiciona os logradouros com índices
            if (model.Logradouros != null && model.Logradouros.Any())
            {
                for (int i = 0; i < model.Logradouros.Count; i++)
                {
                    formData.Add(new StringContent(model.Logradouros[i].Endereco), $"Logradouros[{i}]");
                }
            }

            // Adiciona o arquivo (logotipo), se existir
            if (model.Logotipo != null)
            {
                var fileContent = new StreamContent(model.Logotipo.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Logotipo.ContentType);
                formData.Add(fileContent, "logotipo", model.Logotipo.FileName);
            }

            return formData;
        }
    }
}
