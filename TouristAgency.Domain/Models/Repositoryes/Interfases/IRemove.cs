
namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IRemove<T> where T : class
    {
        void Remove(T obj);
    }
}
