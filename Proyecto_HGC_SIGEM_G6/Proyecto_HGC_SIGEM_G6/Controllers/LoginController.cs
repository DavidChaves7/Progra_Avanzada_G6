using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRestService _restService;
        public LoginController(IRestService restService) => _restService = restService;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string correo, string contrasena)
        {
            correo = (correo ?? string.Empty).Trim().ToLowerInvariant();
            contrasena = (contrasena ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                ViewBag.Mensaje = "Debe ingresar el correo y la contraseña.";
                return View();
            }

            var hash = HashSha256Hex(contrasena);
            var res = await _restService.VerificarCredenciales(correo, hash);

            if (res == null || res.hasErrors || res.data == null)
            {
                ViewBag.Mensaje = "Correo o contraseña incorrectos.";
                return View();
            }
            if (!res.data.Activo)
            {
                ViewBag.Mensaje = "El usuario está inactivo.";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", res.data.IdUsuario);
            HttpContext.Session.SetString("User", res.data.Correo ?? correo);
            HttpContext.Session.SetString("Rol", res.data.Rol ?? "U");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registro() => View(new ModelHelpes.Models.Usuario());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(ModelHelpes.Models.Usuario usuario, string contrasenaPlano)
        {
            if (usuario == null)
            {
                ViewBag.Mensaje = "Datos inválidos.";
                return View(new ModelHelpes.Models.Usuario());
            }

            usuario.IdUsuario = 0;
            usuario.Correo = (usuario.Correo ?? string.Empty).Trim().ToLowerInvariant();
            usuario.FechaRegistro = DateTime.Now;
            usuario.Activo = true;
            usuario.Rol = "U"; 

            var hash = HashSha256Hex((contrasenaPlano ?? string.Empty).Trim());
            if (string.IsNullOrEmpty(hash))
            {
                ViewBag.Mensaje = "Debe proporcionar una contraseña válida.";
                return View(usuario);
            }

            usuario.ContrasenaHash = hash;

            var resultado = await _restService.NuevoUsuario(usuario);
            if (resultado == null || resultado.hasErrors)
            {
                ViewBag.Mensaje = resultado?.errorMessage ?? "No fue posible registrar el usuario.";
                return View(usuario);
            }

            TempData["RegistroExitoso"] = "¡Usuario registrado con éxito! Inicia sesión.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Index", "Login");
        }

        private static string HashSha256Hex(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }
}
