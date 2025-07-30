using ModelHelper.Models;

namespace Proyecto_HGC_SIGEM_G6.Services.Interfaces
{
    public interface IApisExternosService
    {
        Task<ClimaDto> ObtenerClimaAsync(string ciudad);
        Task<DivisaDto> ObtenerDivisasAsync();
    }
}
