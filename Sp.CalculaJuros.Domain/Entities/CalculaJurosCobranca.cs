using Sp.CalculaJuros.Domain.Interfaces;
using System;

namespace Sp.CalculaJuros.Domain.Entities
{
    public class CalculaJurosCobranca : ICalculaJuros<Cobranca>
    {
        public decimal? ValorFinal { get; private set; }
        public string MsgLog { get; private set; }

        public void Calcular(Cobranca Entity)
        {
            try
            {
                if (Entity?.ValorInicial != 0 && Entity?.ValorJuro > 0 && Entity?.Tempo > 0)
                {
                    decimal _juro = (1 + Entity.ValorJuro);
                    decimal juroAcumulado = Convert.ToDecimal(Math.Pow(decimal.ToDouble(_juro), Entity.Tempo));
                    decimal fatorMultiDivisor = Convert.ToDecimal(Math.Pow(10, 2));

                    ValorFinal = (Math.Floor(Entity.ValorInicial * juroAcumulado * fatorMultiDivisor) / fatorMultiDivisor);
                }
                else
                {
                    MsgLog = $"Não foi possível calcular juros, verifique parâmetros: {nameof(Entity.ValorInicial)}: {Entity?.ValorInicial}, " +
                        $"{nameof(Entity.ValorJuro)}: {Entity?.ValorJuro}, " +
                        $"{nameof(Entity.Tempo)}: {Entity?.Tempo}";
                }
            }
            catch (Exception ex)
            {
                MsgLog = ex.Message;
            }

        }
    }
}
