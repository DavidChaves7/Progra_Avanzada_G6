using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ModelHelper.Models;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
