using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_HGC_SIGEM_G6.Context;
using ModelHelpes.Models.Widgets;
using System.Text;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class WidgetsController : Controller
    {
        private readonly DBContext _db;
        public WidgetsController(DBContext db) => _db = db;

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _db.Widgets.AsNoTracking()
                         .OrderBy(w => w.Nombre).ToListAsync();
            return View(list);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Widget model)
        {
           
            model.FrecuenciaRefresco ??= 60;

            if (!ModelState.IsValid)
            {
               
                var sb = new StringBuilder("No fue posible guardar el widget.");
                foreach (var kv in ModelState)
                    foreach (var err in kv.Value.Errors)
                        sb.AppendLine($" {kv.Key}: {err.ErrorMessage}");
                TempData["err"] = sb.ToString();

                var list = await _db.Widgets.AsNoTracking().OrderBy(w => w.Nombre).ToListAsync();
                return View("Index", list);
            }

            _db.Widgets.Add(model);
            await _db.SaveChangesAsync();
            TempData["ok"] = "Widget creado.";

            return RedirectToAction(nameof(Index));
        }

       
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var w = await _db.Widgets.FirstOrDefaultAsync(x => x.IdWidget == id);
            if (w == null) return NotFound();
            return View(w);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Widget model)
        {
            if (!ModelState.IsValid) return View(model);

            var w = await _db.Widgets.FirstOrDefaultAsync(x => x.IdWidget == model.IdWidget);
            if (w == null) return NotFound();

            w.Nombre = model.Nombre;
            w.Tipo = model.Tipo;
            w.UrlApi = model.UrlApi;
            w.ApiKey = model.ApiKey;
            w.FrecuenciaRefresco = model.FrecuenciaRefresco ?? 60;
            w.RutaImagen = model.RutaImagen;
            w.Activo = model.Activo;

            await _db.SaveChangesAsync();
            TempData["ok"] = "Widget actualizado.";
            return RedirectToAction(nameof(Index));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var w = await _db.Widgets.FirstOrDefaultAsync(x => x.IdWidget == id);
            if (w != null)
            {
                _db.Widgets.Remove(w);
                await _db.SaveChangesAsync();
                TempData["ok"] = "Widget eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<IActionResult> Test(int id)
        {
            var w = await _db.Widgets.AsNoTracking().FirstOrDefaultAsync(x => x.IdWidget == id);
            if (w == null) return NotFound();
            return View(w);
        }
    }
}
