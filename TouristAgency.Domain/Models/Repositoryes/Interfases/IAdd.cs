namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IAdd<T> where T : class
    {
        void Add(T obj);
    }
}
