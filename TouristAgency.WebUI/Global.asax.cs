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

        //protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        //{
        //    if (Request.RequestContext.RouteData.Values["controller"] == null)
        //        return;

        //    ApplicationDbContext logRepo = new ApplicationDbContext();
        //    string Controller = (Request.RequestContext.RouteData.Values["controller"] != null ? Request.RequestContext.RouteData.Values["controller"].ToString() : "PanelSpecifications");
        //    string Action = (Request.RequestContext.RouteData.Values["action"] != null ? Request.RequestContext.RouteData.Values["action"].ToString() : "Index");
        //    int counter = 0;
        //    string IP = Request.UserHostAddress;
        //    string UserName = User.Identity.Name;
        //    string GUID = Guid.NewGuid().ToString();

        //    // start mark
        //    logRepo.Logs.Add(new Log()
        //    {
        //        Controller = Controller,
        //        Action = Action,
        //        IP = IP,
        //        UserName = UserName,
        //        Number = counter,
        //        Field = "StartLog",
        //        Value = "StartLog",
        //        GUID = GUID
        //    }
        //    );

        //    counter++;

        //    // full route data
        //    foreach (var item in Request.RequestContext.RouteData.Values)
        //    {
        //        if (item.Key.Trim().ToLower() != "controller"
        //            && item.Key.Trim().ToLower() != "action")
        //        {
        //            logRepo.Logs.Add(new Log()
        //            {
        //                Controller = Controller,
        //                Action = Action,
        //                IP = IP,
        //                UserName = UserName,
        //                Number = counter,
        //                Field = item.Key ?? "",
        //                Value = Convert.ToString(item.Value) ?? "",
        //                GUID = GUID

        //            }
        //            );
        //            counter++;
        //        }
        //    }

        //    // Request Query String
        //    foreach (string key in Request.QueryString.Keys)
        //    {
        //        string Value = Convert.ToString(Request.QueryString[key]);
        //        logRepo.Logs.Add(new Log()
        //        {
        //            Controller = Controller,
        //            Action = Action,
        //            IP = IP,
        //            UserName = UserName,
        //            Number = counter,
        //            Field = key ?? "",
        //            Value = Value ?? "",
        //            GUID = GUID

        //        }
        //        );
        //        counter++;

        //    }

        //    // Request Form Values
        //    foreach (string key in Request.Form.Keys)
        //    {
        //        string Value = Convert.ToString(Request.Form[key]);
        //        logRepo.Logs.Add(new Log()
        //        {
        //            Controller = Controller,
        //            Action = Action,
        //            IP = IP,
        //            UserName = UserName,
        //            Number = counter,
        //            Field = key ?? "",
        //            Value = Value ?? "",
        //            GUID = GUID

        //        }
        //        );
        //        counter++;

        //    }
        //    // save changes
        //    logRepo.SaveChanges();
        //}
    }
}
