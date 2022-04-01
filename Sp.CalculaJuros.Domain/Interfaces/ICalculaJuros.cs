namespace Sp.CalculaJuros.Domain.Interfaces
{
    public interface ICalculaJuros<T> where T : class
    {
        void Calcular(T Entity);
    }
}
