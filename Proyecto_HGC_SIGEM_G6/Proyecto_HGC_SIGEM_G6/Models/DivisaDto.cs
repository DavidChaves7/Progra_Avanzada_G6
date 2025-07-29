using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto_HGC_SIGEM_G6.Models
{
    public class DivisaDto
    {
        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, decimal> Conversion_rates { get; set; }
    }
}
