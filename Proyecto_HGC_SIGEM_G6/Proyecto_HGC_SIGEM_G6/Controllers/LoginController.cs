using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ModelHelper.Models;
using Proyecto_HGC_SIGEM_G6.Context;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRestService _restService;

        public LoginController(IRestService restService)
        {
            _restService = restService;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                ViewBag.Mensaje = "Debe ingresar el correo y la contraseña.";
                return View();
            }

            var res = await _restService.VerificarCredenciales(correo, contrasena);

            if (res.hasErrors || res.data == null)
            {
                ViewBag.Mensaje = "Correo o contraseña incorrectos.";
                return View();
            }

            if (!res.data.Activo)
            {
                ViewBag.Mensaje = "El usuario está inactivo.";
                return View();
            }

            HttpContext.Session.SetString("User", res.data.Correo);
            HttpContext.Session.SetString("Rol", res.data.Rol);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registro() => View();


        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario)
        {

            usuario.IdUsuario = 0;
            usuario.FechaRegistro = DateTime.Now;
            usuario.Activo = true;
            usuario.Rol = "U";

            var resultado = await _restService.NuevoUsuario(usuario);

            if (resultado.hasErrors)
            {
                ViewBag.Mensaje = resultado.errorMessage;
                return View(usuario);
            }

            TempData["RegistroExitoso"] = "¡Usuario registrado con éxito! Inicia sesión.";
            return RedirectToAction("Index");
        }

    }
}
