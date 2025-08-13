using ModelHelper.Models.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHelper.Models.Ordenes
{
    [Table("Ordenes")]
    public class Orden
    {
        [Key] public int IdOrden { get; set; }

        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Required, StringLength(50)]
        public string Estado { get; set; } = "Pendiente"; 

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
