using Microsoft.AspNetCore.Mvc.Testing;
using Sp.CalculaJuros.Domain.Entities;
using System.Net.Http;
using Xunit;

namespace Sp.CalculaJuros.Test
{
    public class CalculaJuros_Tests
    {
        private readonly HttpClient _client;

        public CalculaJuros_Tests()
        {
            var appFactory = new WebApplicationFactory<Web.Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async void showmethecode_Test()
        {
            var resultado = "";
            var response = await _client.GetAsync("showmethecode");
            if (response.IsSuccessStatusCode)
            {
                var texto = response.Content.ReadAsStringAsync();
                resultado = texto.Result;
            }
            Assert.Equal("https://github.com/ivanfpoletto/SPJuros", resultado);
        }

        [Fact]
        public void CalculaJuros_Test()
        {
            Assert.Equal(105.1M, getValorFinalTest(100, 5, 0.01M));
            Assert.Equal(122.01M, getValorFinalTest(100, 20, 0.01M));
            Assert.Equal(244.03M, getValorFinalTest(200, 20, 0.01M));
            Assert.Equal(142.82M, getValorFinalTest(100, 18, 0.02M));
            Assert.Equal(-142.83M, getValorFinalTest(-100, 18, 0.02M));

            decimal? valorNulo = null;
            Assert.Equal(valorNulo, getValorFinalTest(100, 18, 0));
            Assert.Equal(valorNulo, getValorFinalTest(0, 18, 0.01M));
            Assert.Equal(valorNulo, getValorFinalTest(100, 0, 0.01M));
        }

        private decimal? getValorFinalTest(decimal _valorInicial, int _tempo, decimal _valorJuro)
        {
            var cobranca = new Cobranca
            {
                ValorInicial = _valorInicial,
                Tempo = _tempo,
                ValorJuro = _valorJuro
            };
            var calculaJurosCobranca = new CalculaJurosCobranca();
            calculaJurosCobranca.Calcular(cobranca);
            return calculaJurosCobranca.ValorFinal;
        }

        [Fact]
        public async void Taxajuros_Test()
        {
            var appFactory = new WebApplicationFactory<Sp.TaxaJuros.Web.Startup>();
            var _clientTaxa = appFactory.CreateClient();

            var resultado = "";
            var response = await _clientTaxa.GetAsync("/taxaJuros");
            if (response.IsSuccessStatusCode)
            {
                var texto = response.Content.ReadAsStringAsync();
                resultado = texto.Result;
            }
            Assert.Equal("0,01", resultado);
        }

    }
}
