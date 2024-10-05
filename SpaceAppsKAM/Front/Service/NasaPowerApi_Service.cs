using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Front.Service
{
    public class NasaPowerApi_Service
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NasaPowerApi_Service> _logger;

        public async Task<string> GetNasaPowerDataAsync(string parameters)
        {
            try
            {
                // URL base de la API de NASA POWER
                string url = $"https://power.larc.nasa.gov/api/temporal/daily/point?{parameters}";

                // Realizar la solicitud HTTP GET
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                else
                {
                    _logger.LogError($"Error al obtener datos de NASA POWER: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Excepción al hacer la solicitud: {ex.Message}");
                return null;
            }
        }
    }
}


