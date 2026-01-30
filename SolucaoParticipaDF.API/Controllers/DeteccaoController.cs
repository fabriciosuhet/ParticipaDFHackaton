using Microsoft.AspNetCore.Mvc;
using SolucaoParticipaDF.API.Services.Interfaces;

namespace SolucaoParticipaDF.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeteccaoController : ControllerBase
    {
        private readonly ILeitorXlsxService _leitorXlsxService;
        private readonly IOrquestradorDeteccaoService _orquestradorService;

        public DeteccaoController(
            ILeitorXlsxService leitorXlsxService,
            IOrquestradorDeteccaoService orquestradorService)
        {
            _leitorXlsxService = leitorXlsxService;
            _orquestradorService = orquestradorService;
        }

        [HttpPost("xlsx")]
        public IActionResult ProcessarXlsx(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Arquivo XLSX não informado.");

            using var stream = arquivo.OpenReadStream();

            var pedidos = _leitorXlsxService.LerPedidos(stream);

            var resultados = pedidos.Select(p =>
                _orquestradorService.Processar(p.Texto)
            );

            return Ok(resultados);
        }
    }
}
