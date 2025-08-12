using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelHelpes.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key] public int IdUsuario { get; set; }

        [Required, MaxLength(100)] public string Nombre { get; set; } = string.Empty;
        [Required, MaxLength(100)] public string Correo { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        [Column("ContraseñaHash")]  
        public string ContrasenaHash { get; set; } = string.Empty;

        [Required, MaxLength(50)] public string Rol { get; set; } = "U";
        public DateTime? FechaRegistro { get; set; }
        public bool Activo { get; set; } = true;
    }
}
