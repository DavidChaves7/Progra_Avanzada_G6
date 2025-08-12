using Microsoft.AspNetCore.Mvc;
using Proyecto_HGC_SIGEM_G6.Services.Interfaces;

namespace Proyecto_HGC_SIGEM_G6.Controllers
{
    [ApiController]
    [Route("api/ext")]
    public class ExtController : ControllerBase
    {
        private readonly IApisExternosService _apis;

        public ExtController(IApisExternosService apis)
        {
            _apis = apis;
        }

        
        [HttpGet("ok")]
        public IActionResult OkPing() => Ok(new { ok = true, at = DateTime.UtcNow });

        
        [HttpGet("clima")]
        public async Task<IActionResult> GetClima([FromQuery] string city = "Alajuela")
        {
            var dto = await _apis.ObtenerClimaAsync(city);
            if (dto == null) return StatusCode(502, new { error = "No se pudo obtener clima" });
            return Ok(dto); 
        }

        
        [HttpGet("divisas")]
        public async Task<IActionResult> GetDivisas([FromQuery] string baseCode = "USD", [FromQuery] string symbols = "CRC")
        {
            var dto = await _apis.ObtenerDivisasAsync();
            if (dto == null) return StatusCode(502, new { error = "No se pudo obtener divisas" });
            return Ok(dto);
        }
    }
}
