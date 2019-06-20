
namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IRepository<T>:IAdd<T>,IFind<T>,IModified<T>,IRemove<T>,ISave,IToList<T> where T:class { }
}
