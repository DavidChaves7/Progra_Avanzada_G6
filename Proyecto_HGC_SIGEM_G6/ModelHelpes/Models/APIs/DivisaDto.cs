using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ModelHelper.Models
{
    public class DivisaDto
    {
        [JsonPropertyName("conversion_rates")]
        public Dictionary<string, decimal> Conversion_rates { get; set; }
    }
}
