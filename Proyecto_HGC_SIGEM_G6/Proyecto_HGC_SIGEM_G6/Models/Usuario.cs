using System.ComponentModel.DataAnnotations;

namespace Proyecto_HGC_SIGEM_G6.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required, EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Contrasena { get; set; }

        [Required]
        public string Rol { get; set; }
    }
}
