using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgency.Domain.Models.Repositoryes.Interfases
{
    public interface IGetContext<T> where T:class
    {
        T GetContext();
    }
}
