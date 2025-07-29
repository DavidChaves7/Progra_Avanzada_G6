using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Proyecto_HGC_SIGEM_G6.Models;

namespace Proyecto_HGC_SIGEM_G6.Services
{
    public class ClimaService
    {
        private readonly HttpClient _httpClient;

        public ClimaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClimaDto> ObtenerClimaAsync(string ciudad)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid=92a2affa57e47178b671b680bd2d2925&units=metric";

            return await _httpClient.GetFromJsonAsync<ClimaDto>(url);
        }
    }
}
