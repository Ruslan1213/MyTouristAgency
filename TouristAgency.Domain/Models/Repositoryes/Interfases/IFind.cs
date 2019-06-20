
namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IFind<T> where T: class
    {
        T Find(int? id);
    }
}
