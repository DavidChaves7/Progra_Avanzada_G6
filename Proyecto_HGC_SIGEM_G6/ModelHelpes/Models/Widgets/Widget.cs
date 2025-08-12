using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelHelpes.Models.Widgets
{
    [Table("Widgets")]
    public class Widget
    {
        [Key]
        public int IdWidget { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Tipo { get; set; } = "Chart"; 

        [MaxLength(255)]
        public string? UrlApi { get; set; }

        [MaxLength(255)]
        public string? ApiKey { get; set; }

        public int? FrecuenciaRefresco { get; set; } 

        [MaxLength(255)]
        public string? RutaImagen { get; set; }

        public bool Activo { get; set; } = true;
    }
}
