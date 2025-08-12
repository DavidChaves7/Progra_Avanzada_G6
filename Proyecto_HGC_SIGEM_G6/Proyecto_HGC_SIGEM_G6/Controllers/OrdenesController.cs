using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_HGC_SIGEM_G6.Context;
using ModelHelper.Models.Productos;
using ModelHelper.Models.Ordenes;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly DBContext _db;
        public OrdenesController(DBContext db) => _db = db;

        // GET: /Ordenes
        public async Task<IActionResult> Index()
        {
            var data = await _db.Ordenes
                .Include(o => o.Producto)
                .OrderByDescending(o => o.Fecha)
                .ToListAsync();
            return View(data);
        }

        // GET: /Ordenes/Crear
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Productos = await _db.Productos
                .Where(p => p.Activo && p.Cantidad > 0)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
            return View(new Orden { Cantidad = 1 });
        }

        // POST: /Ordenes/Crear
        [HttpPost]
        public async Task<IActionResult> Crear(int IdProducto, int Cantidad)
        {
            var prod = await _db.Productos.FirstOrDefaultAsync(p => p.IdProducto == IdProducto && p.Activo);
            if (prod is null)
            {
                ModelState.AddModelError("", "Producto no encontrado o inactivo.");
            }
            else if (Cantidad <= 0 || Cantidad > prod.Cantidad)
            {
                ModelState.AddModelError("", $"Cantidad inválida. Disponible: {prod.Cantidad}.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Productos = await _db.Productos.Where(p => p.Activo && p.Cantidad > 0).OrderBy(p => p.Nombre).ToListAsync();
                return View(new Orden { IdProducto = IdProducto, Cantidad = Cantidad });
            }

            var orden = new Orden
            {
                IdUsuario = 0, 
                IdProducto = prod!.IdProducto,
                Cantidad = Cantidad,
                Total = prod.Precio * Cantidad,
                Estado = "Pendiente",
                Fecha = DateTime.Now
            };

            // Descontar stock
            prod.Cantidad -= Cantidad;

            _db.Ordenes.Add(orden);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: /Ordenes/Cancelar/5  (restaura stock)
        [HttpPost]
        public async Task<IActionResult> Cancelar(int id)
        {
            var o = await _db.Ordenes.FirstOrDefaultAsync(x => x.IdOrden == id);
            if (o is null) return NotFound();

            if (o.Estado != "Cancelada")
            {
                var prod = await _db.Productos.FindAsync(o.IdProducto);
                if (prod != null) prod.Cantidad += o.Cantidad; 
                o.Estado = "Cancelada";
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
