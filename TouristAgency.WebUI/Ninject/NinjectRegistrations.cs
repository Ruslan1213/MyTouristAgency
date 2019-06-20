using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TouristAgency.Domain.Models.EfModels;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using TouristAgency.Domain.Models.Repositoryes.Repositiry;
using TouristAgency.WebUI.Models;

namespace TouristAgency.WebUI.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Tour>>().To<TourRepository>();
            Bind<IRepository<Journey>>().To<JourneysRepository>();

            Bind<IRepositoryForUsers<ApplicationMyUser>>().To<ApplicationMyUsersRepository>();
            Bind<IRepository<OrderStatus>>().To<OrderStatusRepository>();
            Bind<IRepository<Order>>().To<OrderRepository>();
        }
    }
}