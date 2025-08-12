using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHelper.Models.Productos
{
    [Table("Productos")]
    public class Producto
    {
        [Key] public int IdProducto { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; } = "";

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        public bool Activo { get; set; }
    }
}
