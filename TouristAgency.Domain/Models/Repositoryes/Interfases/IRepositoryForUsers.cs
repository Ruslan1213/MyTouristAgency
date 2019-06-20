using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IRepositoryForUsers<T>:IAdd<T>,IFind<T>,IModified<T>,IRemove<T>,ISave,IToList<T> where T:class
    {
        T FindUser(string id);
    }
}
