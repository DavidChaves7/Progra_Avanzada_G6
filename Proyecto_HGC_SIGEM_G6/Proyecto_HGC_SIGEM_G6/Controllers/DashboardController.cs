using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_HGC_SIGEM_G6.Context;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces; 
using ModelHelper.Models.Dashboard;
using ModelHelper.Models.Productos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DBContext _db;
        private readonly IApisExternosService _apis;

        public DashboardController(DBContext db, IApisExternosService apis)
        {
            _db = db;
            _apis = apis;
        }

        public async Task<IActionResult> Index()
        {
            var hoy = DateTime.Today;

            // KPIs de productos
            var totalProductos = await _db.Productos.CountAsync();
            var productosActivos = await _db.Productos.CountAsync(p => p.Activo);
            var productosBajoStock = await _db.Productos.CountAsync(p => p.Cantidad <= 5);

            // KPIs de órdenes
            var totalOrdenes = await _db.Ordenes.CountAsync();
            var ordenesPendientes = await _db.Ordenes.CountAsync(o => o.Estado == "Pendiente");
            var ventasHoy = await _db.Ordenes
                .Where(o => o.Fecha.Date == hoy && o.Estado != "Cancelada")
                .SumAsync(o => (decimal?)o.Total) ?? 0m;

            // Listados
            var ultimosProductos = await _db.Productos
                .OrderByDescending(p => p.IdProducto)
                .Take(5).ToListAsync();

            var ultimasOrdenes = await _db.Ordenes
                .Include(o => o.Producto)
                .OrderByDescending(o => o.Fecha)
                .Take(6).ToListAsync();

            // Top productos vendidos
            var topAgg = await _db.Ordenes
                .Where(o => o.Estado != "Cancelada")
                .GroupBy(o => o.IdProducto)
                .Select(g => new { IdProducto = g.Key, Cant = g.Sum(x => x.Cantidad), Monto = g.Sum(x => x.Total) })
                .OrderByDescending(x => x.Cant)
                .Take(5)
                .ToListAsync();

            var topJoin = await _db.Productos
                .Where(p => topAgg.Select(t => t.IdProducto).Contains(p.IdProducto))
                .ToListAsync();

            var topFinal = topAgg
                .Select(t => (topJoin.First(p => p.IdProducto == t.IdProducto), t.Cant, t.Monto))
                .Select(x => (Producto: x.Item1, CantidadVendida: x.Cant, Monto: x.Monto))
                .ToList();

            // APIs externas
            var clima = await _apis.ObtenerClimaAsync("Alajuela");
            var divisa = await _apis.ObtenerDivisasAsync();

            // Si tienes una lista de muebles, cárgala aquí. Si no, queda vacío.
            var vm = new DashboardViewModel
            {
                TotalProductos = totalProductos,
                ProductosActivos = productosActivos,
                ProductosBajoStock = productosBajoStock,
                TotalOrdenes = totalOrdenes,
                OrdenesPendientes = ordenesPendientes,
                VentasHoy = ventasHoy,
                UltimosProductos = ultimosProductos,
                UltimasOrdenes = ultimasOrdenes,
                TopProductos = topFinal,
                Clima = clima,
                Divisa = divisa
            };

            return View(vm);
        }
    }
}
