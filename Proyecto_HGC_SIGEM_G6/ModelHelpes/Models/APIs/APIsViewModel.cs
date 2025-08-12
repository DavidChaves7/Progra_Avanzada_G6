using ModelHelpes.Models.APIs;

namespace ModelHelper.Models
{
    public class APIsViewModel
    {
        public ClimaDto Clima { get; set; }
        public DivisaDto Divisa { get; set; }
        public List<TrendItem> Muebles { get; set; } = new();
    }
}
