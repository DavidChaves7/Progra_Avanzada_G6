using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Proyecto_HGC_SIGEM_G6.Models;

namespace Proyecto_HGC_SIGEM_G6.Services
{
    public class DivisaService
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://v6.exchangerate-api.com/v6/8a133b3278dd4297e86c5913/latest/USD";

        public DivisaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DivisaDto> ObtenerDivisasAsync()
        {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DivisaDto>(json);
        }
    }
}
