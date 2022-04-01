using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sp.CalculaJuros.Domain.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sp.CalculaJuros.Web.Controllers
{
    public class CalculaJurosController : Controller
    {
        private readonly ILogger<CalculaJurosController> _logger;
        private readonly IConfiguration _config;
        public CalculaJurosController(IConfiguration config, ILogger<CalculaJurosController> logger)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// Obter taxa de Juro
        /// </summary>
        /// <response code="200">Taxa de juro</response>
        /// <response code="500">Ocorreu um erro ao obter taxa de juro</response>
        /// <param name="valorinicial">valor inicial que será aplicado juro</param>
        /// <param name="meses">quantidade de meses para incrementar juros</param>
        /// <returns>Retorna o valor aplicado juro e meses informado. Em caso de erro ou falta de informacao retorna null.</returns>
        [HttpGet]
        [Route("calculajuros")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(500)]
        public async Task<string> CalculaJuros(decimal valorinicial, int meses)
        {
            var cobranca = new Cobranca();
            cobranca.ValorInicial = valorinicial;
            cobranca.Tempo = meses;
            cobranca.ValorJuro = await GetTaxaJuros();

            var calculaJurosCobranca = new CalculaJurosCobranca();
            calculaJurosCobranca.Calcular(cobranca);
            if (calculaJurosCobranca?.ValorFinal != null)
            {
                return $"{calculaJurosCobranca.ValorFinal:N2}";
            }
            else
            {
                _logger.LogError(calculaJurosCobranca?.MsgLog);
                return null;
            }
        }

        /// <summary>
        /// Mostra a url de onde encontra-se o fonte no github
        /// </summary>
        /// <response code="200">Mostra url</response>
        /// <response code="500">Ocorreu ao carregar url</response>
        [HttpGet]
        [Route("showmethecode")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(500)]
        public string ShowMeTheCode()
        {
            return "https://github.com/ivanfpoletto/SPJuros";
        }

        /// <summary>
        /// Metodo para retornar taxa de juros da API TaxaJuros
        /// </summary>
        /// <returns>Retorna o valor aplicado juro e meses informado. Em caso de erro ou falta de informacao retorna null.</returns>
        private async Task<decimal> GetTaxaJuros()
        {
            try
            {
                var httpClient = new HttpClient();
                var urlJuros = $"{_config.GetSection("UrlJuros").Value}/taxaJuros";

                var response = await httpClient.GetAsync(urlJuros);

                if (response.IsSuccessStatusCode)
                {
                    var texto = response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(texto.Result))
                    {
                        var juroAPI = Convert.ToDecimal(texto.Result);
                        if (juroAPI > 0)
                        {
                            return juroAPI;
                        }
                    }
                }
            }
            catch
            {
            }
            return 0;
        }
    }
}
