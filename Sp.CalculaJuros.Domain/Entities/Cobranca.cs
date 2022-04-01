namespace Sp.CalculaJuros.Domain.Entities
{
    public class Cobranca
    {
        public decimal ValorInicial { get; set; }
        public int Tempo { get; set; }
        public decimal ValorJuro { get; set; }
    }
}
