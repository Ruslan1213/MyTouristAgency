using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TouristAgency.WebUI.Models;
using TouristAgency.Domain.Models.InitializerDb;
using Ninject.Modules;
using TouristAgency.WebUI.Ninject;
using Ninject;
using Ninject.Web.Mvc;
using TouristAgency.Domain.Models.EfModels;

namespace TouristAgency.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbContextInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
