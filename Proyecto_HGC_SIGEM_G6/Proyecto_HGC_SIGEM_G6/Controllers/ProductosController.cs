using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_HGC_SIGEM_G6.Context;
using ModelHelper.Models.Productos;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DBContext _db;
        public ProductosController(DBContext db) => _db = db;

        // GET: /Productos
        public async Task<IActionResult> Index()
        {
            var productos = await _db.Productos
                .OrderByDescending(p => p.Activo)
                .ThenBy(p => p.Nombre)
                .ToListAsync();
            return View(productos);
        }

        // GET: /Productos/Crear
        [HttpGet]
        public IActionResult Crear() => View(new Producto { Activo = true });

        // POST: /Productos/Crear
        [HttpPost]
        public async Task<IActionResult> Crear(Producto model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Productos.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Productos/Editar/5
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p is null) return NotFound();
            return View(p);
        }

        // POST: /Productos/Editar/5
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Producto model)
        {
            if (id != model.IdProducto) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Productos/Toggle/5  (activar/desactivar)
        [HttpPost]
        public async Task<IActionResult> Toggle(int id)
        {
            var p = await _db.Productos.FindAsync(id);
            if (p is null) return NotFound();
            p.Activo = !p.Activo;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
