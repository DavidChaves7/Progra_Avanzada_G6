using Microsoft.AspNetCore.Mvc;
using Proyecto_HGC_SIGEM_G6.Services;
using Proyecto_HGC_SIGEM_G6.Models;
using System.Threading.Tasks;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class APIsController : Controller
    {
        private readonly ClimaService _climaService;
        private readonly DivisaService _divisaService;

        public APIsController(ClimaService climaService, DivisaService divisaService)
        {
            _climaService = climaService;
            _divisaService = divisaService;
        }

        public async Task<IActionResult> Index()
        {
            var clima = await _climaService.ObtenerClimaAsync("Alajuela");
            var divisa = await _divisaService.ObtenerDivisasAsync();

            var viewModel = new APIsViewModel
            {
                Clima = clima,
                Divisa = divisa
            };

            return View(viewModel);
        }
    }
}
