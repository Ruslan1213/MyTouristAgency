using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TouristAgency.Domain.App_Start;
using TouristAgency.Domain.Filters;
using TouristAgency.WebUI.Models;

namespace TouristAgency.WebUI.Controllers
{
    public class HomeController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(HomeController));


        [AllowAnonymous]
        public ActionResult Index()
        {
            //    ApplicationMyUser myUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //    if (myUser != null)
            //        if (myUser.IsBloked != null)
            //            if ((bool)myUser.IsBloked)
            //            {
            //                //var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            //                //AuthenticationManager.SignOut();
            //                ViewBag.Error = true;
            //                //return Redirect("Account/Login");
            //                return View(ViewBag.Error);
            //            }
            //log.Debug("Debug message");
            //log.Warn("Warn message");
            //log.Error("Error message");
            //log.Fatal("Fatal message");
            return View();
        }
        [AdminAction]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NoAccessRights()
        {
            return View();
        }

        [Authorize]
        public ActionResult My()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            string userId = User.Identity.GetUserId();

            // get user roles
            List<string> roles = userManager.GetRoles(userId).ToList();

            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationMyUser mUser = db.Users.Find(user);
            if (mUser != null)
            {
                if (System.Web.HttpContext.Current.User.IsInRole("admin"))
                    return View("AdminCabinet", mUser);
                else if (System.Web.HttpContext.Current.User.IsInRole("manager"))
                    return View("ManagerCabinet", mUser);
                else
                    return View("MyCabinet", mUser);
            }
            else return View("Index");
        }

        private bool IsAdmin(ApplicationMyUser mUser, List<string> roles)
        {
            foreach (var role in roles)
            {
                if (role == "admin")
                    return true;
            }
            return false;
        }

        private bool IsManager(ApplicationMyUser mUser, List<string> roles)
        {
            foreach (var role in roles)
            {
                if (role == "manager")
                    return true;
            }
            return false;
        }
    }
}