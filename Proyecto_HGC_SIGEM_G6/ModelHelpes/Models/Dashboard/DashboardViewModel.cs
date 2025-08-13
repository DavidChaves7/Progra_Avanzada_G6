using ModelHelpes.Models.APIs;        
using ModelHelper.Models.Ordenes;       
using ModelHelper.Models.Productos;     
using System.Collections.Generic;

namespace ModelHelper.Models.Dashboard
{
    public class DashboardViewModel
    {
       
        public int TotalProductos { get; set; }
        public int ProductosActivos { get; set; }
        public int ProductosBajoStock { get; set; }
        public int TotalOrdenes { get; set; }
        public int OrdenesPendientes { get; set; }
        public decimal VentasHoy { get; set; }

        public List<Producto> UltimosProductos { get; set; } = new();
        public List<Orden> UltimasOrdenes { get; set; } = new();
        public List<(Producto Producto, int CantidadVendida, decimal Monto)> TopProductos { get; set; } = new();

        public ClimaDto? Clima { get; set; }
        public DivisaDto? Divisa { get; set; }
        public List<TrendItem> Muebles { get; set; } = new();
    }
}

