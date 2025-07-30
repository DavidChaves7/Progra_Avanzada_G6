using Microsoft.AspNetCore.Mvc;
using Proyecto_HGC_SIGEM_G6.Services;
using ModelHelper.Models;
using System.Threading.Tasks;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class APIsController : Controller
    {
        private readonly IApisExternosService _apisEService;

        public APIsController(IApisExternosService apisEService)
        {
            _apisEService = apisEService;
        }

        public async Task<IActionResult> Index()
        {
            var clima = await _apisEService.ObtenerClimaAsync("Alajuela");
            var divisa = await _apisEService.ObtenerDivisasAsync();

            var viewModel = new APIsViewModel
            {
                Clima = clima,
                Divisa = divisa
            };

            return View(viewModel);
        }
    }
}
