using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristAgency.Domain.App_Start;
using TouristAgency.Domain.Models.UserCreatrdByAdmin;
using TouristAgency.WebUI.Models;
using System.Data.Entity;
using log4net;

namespace TouristAgency.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(AdminController));
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserCreatedByAdmin user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationMyUser>(db));
            var identityRole = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var adminsUser= new ApplicationMyUser { Email = user.Email, UserName = user.Email ,IsBloked=false};
            var result = userManager.Create(adminsUser, user.Password);
            if (result.Succeeded)
            {
                userManager.AddToRole(adminsUser.Id, identityRole.FindByName(user.Role).Name);
                log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " добавил пользователя " + user.Email + " c ролью " + user.Role);
            }
            db.SaveChanges();
            return Redirect("/Home/My");
        }
    }
}