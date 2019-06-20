using System.Collections.Generic;

namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IToList<T> where T: class
    {
        IEnumerable<T> ToList();
    }
}
