using SIITest.Core.Domain.Entities;
using SIITest.Core.Interfaces;
using Newtonsoft.Json;

namespace SIITest.Infrastructure
{
    public class ExternalService : IExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalService> _logger;
        private readonly string _apiBaseUrl;

        public ExternalService(HttpClient httpClient, ILogger<ExternalService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apiBaseUrl = configuration["ApiBaseUrl"] ?? throw new ArgumentNullException("La URL base de la API no está configurada.");
        }

        public async Task<List<Products>> ObtenerDatosAsync()
        {
            return await GetFromApiAsync<List<Products>>("products");
        }

        public async Task<Products> ObtenerDatosByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"ID inválido: {id}");
                return null;
            }

            return await GetFromApiAsync<Products>($"products/{id}");
        }

        private async Task<T> GetFromApiAsync<T>(string endpoint) where T : class
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_apiBaseUrl}/{endpoint}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error al obtener datos ({endpoint}): {response.StatusCode}");
                    return null;
                }

                string jsonObject = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonObject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Excepción en la llamada a {endpoint}: {ex.Message}");
                return null;
            }
        }

    }
}
