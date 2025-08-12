using ModelHelper.Models;
using ModelHelpes.Models.APIs;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using ModelHelper.Models;


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

        public async Task<List<TrendItem>> ObtenerMueblesAsync(int top = 10)
        {
            // DummyJSON: categoría "furniture" (no requiere API key)
            var url = $"https://dummyjson.com/products/category/furniture?limit={Math.Max(1, top)}";

            using var resp = await _httpClient.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return new List<TrendItem>();

            using var stream = await resp.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            // Respuesta: { products: [ { title, thumbnail, images[] } ] }
            if (!doc.RootElement.TryGetProperty("products", out var products))
                return new List<TrendItem>();

            var items = products.EnumerateArray()
                .Select(p =>
                {
                    string title = p.TryGetProperty("title", out var tt) ? tt.GetString() ?? "" : "";
                    string? img = null;

                    if (p.TryGetProperty("thumbnail", out var th) && !string.IsNullOrWhiteSpace(th.GetString()))
                        img = th.GetString();
                    else if (p.TryGetProperty("images", out var imgs) && imgs.ValueKind == JsonValueKind.Array)
                        img = imgs.EnumerateArray().Select(x => x.GetString()).FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));

                    return new TrendItem(img ?? "", title);
                })
                .Where(i => !string.IsNullOrWhiteSpace(i.ImageUrl) && !string.IsNullOrWhiteSpace(i.Title))
#if NET6_0_OR_GREATER
                .DistinctBy(i => i.ImageUrl)
#else
        .GroupBy(i => i.ImageUrl).Select(g => g.First())
#endif
                .Take(top)
                .ToList();

            return items;
        }


    }
}
