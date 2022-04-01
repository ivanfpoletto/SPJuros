using Microsoft.AspNetCore.Mvc;

namespace Sp.TaxaJuros.Web.Controllers
{
    [ApiController]
    [Route("")]
    public class TaxaJurosController : Controller
    {
        public TaxaJurosController()
        {

        }

        /// <summary>
        /// Obter taxa de Juro
        /// </summary>
        /// <response code="200">Taxa de juro</response>
        /// <response code="500">Ocorreu um erro ao obter taxa de juro</response>
        [HttpGet]
        [Route("taxaJuros")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(500)]
        public string GetTaxaJuros()
        {
            decimal? juro = 0.01M;
            return $"{juro:N2}";
        }
    }
}
