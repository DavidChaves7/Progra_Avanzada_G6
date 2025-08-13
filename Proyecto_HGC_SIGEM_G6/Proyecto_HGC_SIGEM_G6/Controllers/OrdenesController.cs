using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelHelper.Models.Ordenes;
using Proyecto_HGC_SIGEM_G6.Context;
using System.Text;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly DBContext _db;
        public OrdenesController(DBContext db) => _db = db;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _db.Ordenes.AsNoTracking()
                         .OrderBy(w => w.IdOrden).ToListAsync();
            return View(list);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Orden model)
        {

            model.Fecha = DateTime.Now;
            var precio = _db.Productos.FirstOrDefault(p => p.IdProducto == model.IdProducto).Precio;
            model.Total = model.Cantidad * precio;
            model.Estado = "Pendiente";

            if (!ModelState.IsValid)
            {
                var sb = new StringBuilder("No fue posible guardar el Orden.");
                foreach (var kv in ModelState)
                    foreach (var err in kv.Value.Errors)
                        sb.AppendLine($" {kv.Key}: {err.ErrorMessage}");
                TempData["err"] = sb.ToString();

                var list = await _db.Ordenes.AsNoTracking().OrderBy(w => w.IdOrden).ToListAsync();
                return View("Index", list);
            }

            _db.Ordenes.Add(model);
            await _db.SaveChangesAsync();
            TempData["ok"] = "Orden creada.";

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var w = await _db.Ordenes.FirstOrDefaultAsync(x => x.IdOrden == id);
            if (w == null) return NotFound();
            return View(w);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Orden model)
        {
            if (!ModelState.IsValid) return View(model);

            var w = await _db.Ordenes.FirstOrDefaultAsync(x => x.IdOrden == model.IdOrden);
            if (w == null) return NotFound();

            w.Cantidad = model.Cantidad;
            w.Estado = model.Estado;
            var precio = _db.Productos.FirstOrDefault(p => p.IdProducto == model.IdProducto).Precio;
            w.Total = model.Cantidad * precio;
            w.Fecha = DateTime.Now;

            await _db.SaveChangesAsync();
            TempData["ok"] = "Orden actualizado.";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var w = await _db.Ordenes.FirstOrDefaultAsync(x => x.IdOrden == id);
            if (w != null)
            {
                _db.Ordenes.Remove(w);
                await _db.SaveChangesAsync();
                TempData["ok"] = "Orden eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Test(int id)
        {
            var w = await _db.Ordenes.AsNoTracking().FirstOrDefaultAsync(x => x.IdOrden == id);
            if (w == null) return NotFound();
            return View(w);
        }
    }
}
