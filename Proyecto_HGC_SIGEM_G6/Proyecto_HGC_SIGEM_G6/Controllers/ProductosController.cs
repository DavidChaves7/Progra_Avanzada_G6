using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelHelper.Models.Productos;
using Proyecto_HGC_SIGEM_G6.Context;
using System.Text;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DBContext _db;
        public ProductosController(DBContext db) => _db = db;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _db.Productos.AsNoTracking()
                         .OrderBy(w => w.IdProducto).ToListAsync();
            return View(list);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Producto model)
        {

            model.Cantidad = 0;
            model.Activo = true;

            if (!ModelState.IsValid)
            {
                var sb = new StringBuilder("No fue posible guardar el Producto.");
                foreach (var kv in ModelState)
                    foreach (var err in kv.Value.Errors)
                        sb.AppendLine($" {kv.Key}: {err.ErrorMessage}");
                TempData["err"] = sb.ToString();

                var list = await _db.Productos.AsNoTracking().OrderBy(w => w.IdProducto).ToListAsync();
                return View("Index", list);
            }

            _db.Productos.Add(model);
            await _db.SaveChangesAsync();
            TempData["ok"] = "Producto creada.";

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var w = await _db.Productos.FirstOrDefaultAsync(x => x.IdProducto == id);
            if (w == null) return NotFound();
            return View(w);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Producto model)
        {
            if (!ModelState.IsValid) return View(model);

            var w = await _db.Productos.FirstOrDefaultAsync(x => x.IdProducto == model.IdProducto);
            if (w == null) return NotFound();

            w.Cantidad = 0;
            w.Nombre = model.Nombre;
            w.Precio = model.Precio;
            w.Activo = model.Activo;

            await _db.SaveChangesAsync();
            TempData["ok"] = "Producto actualizado.";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var w = await _db.Productos.FirstOrDefaultAsync(x => x.IdProducto == id);
            if (w != null)
            {
                _db.Productos.Remove(w);
                await _db.SaveChangesAsync();
                TempData["ok"] = "Producto eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Test(int id)
        {
            var w = await _db.Productos.AsNoTracking().FirstOrDefaultAsync(x => x.IdProducto == id);
            if (w == null) return NotFound();
            return View(w);
        }
    }
}
