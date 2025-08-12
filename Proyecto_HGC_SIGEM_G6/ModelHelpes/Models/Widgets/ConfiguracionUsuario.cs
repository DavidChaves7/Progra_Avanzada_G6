using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelHelpes.Models.Widgets
{
    [Table("ConfiguracionUsuario")]
    public class ConfiguracionUsuario
    {
        [Key]
        public int IdConfiguracion { get; set; }

        public int IdUsuario { get; set; }
        public int IdWidget { get; set; }

        public int? Posicion { get; set; } = 0;
        public bool Favorito { get; set; } = false;
        public bool Visible { get; set; } = true;
    }
}
