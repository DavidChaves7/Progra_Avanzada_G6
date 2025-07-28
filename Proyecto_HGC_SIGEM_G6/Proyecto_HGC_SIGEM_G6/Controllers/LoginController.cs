using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Proyecto_HGC_SIGEM_G6.Data;
using Proyecto_HGC_SIGEM_G6.Models;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string correo, string contrasena)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contrasena == contrasena);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Usuario", usuario.Nombre);
                HttpContext.Session.SetString("Rol", usuario.Rol);
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Mensaje = "Credenciales inválidas";
            return View();
        }

        [HttpGet]
        public IActionResult Registro() => View();

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Rol = "Usuario"; // los registrados desde aquí siempre serán usuarios normales
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
