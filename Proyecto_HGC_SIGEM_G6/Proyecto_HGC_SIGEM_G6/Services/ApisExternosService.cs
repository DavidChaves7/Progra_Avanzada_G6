using ModelHelper.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;

namespace Proyecto_HGC_SIGEM_G6.Services
{
    public class ApisExternosService : IApisExternosService
    {
        private readonly HttpClient _httpClient;

        public ApisExternosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClimaDto> ObtenerClimaAsync(string ciudad)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid=92a2affa57e47178b671b680bd2d2925&units=metric";

            return await _httpClient.GetFromJsonAsync<ClimaDto>(url);
        }

        public async Task<DivisaDto> ObtenerDivisasAsync()
        {

            var response = await _httpClient.GetAsync("https://v6.exchangerate-api.com/v6/8a133b3278dd4297e86c5913/latest/USD");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DivisaDto>(json);
        }
    }
}
