
namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IModified<T> where T : class
    {
        void Modified(T obj);
    }
}
