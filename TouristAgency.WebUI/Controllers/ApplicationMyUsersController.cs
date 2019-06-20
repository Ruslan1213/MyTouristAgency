using log4net;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TouristAgency.Domain.App_Start;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using TouristAgency.Domain.Models.Repositoryes.Repositiry;
using TouristAgency.WebUI.Models;

namespace TouristAgency.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ApplicationMyUsersController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(ApplicationMyUsersController));
        private IRepositoryForUsers<ApplicationMyUser> applicationMyUserRepository;

        public ApplicationMyUsersController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoryForUsers<ApplicationMyUser>>().To<ApplicationMyUsersRepository>();
            applicationMyUserRepository = ninjectKernel.Get<IRepositoryForUsers<ApplicationMyUser>>();;
        }

        // GET: ApplicationMyUsers
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(applicationMyUserRepository.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: ApplicationMyUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationMyUser applicationMyUser = applicationMyUserRepository.FindUser(id);
            if (applicationMyUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationMyUser);
        }

        // GET: ApplicationMyUsers/Create
        public ActionResult Create()
        {
            return Redirect("/Admin/AddUser"); ;
        }

        // POST: ApplicationMyUsers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: ApplicationMyUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationMyUser applicationMyUser = applicationMyUserRepository.FindUser(id);
            if (applicationMyUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationMyUser);
        }

        // POST: ApplicationMyUsers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsBloked,Email,UserName")] ApplicationMyUser applicationMyUser)
        {
            ApplicationMyUser user = applicationMyUserRepository.FindUser(applicationMyUser.Id);
            if (user != null)
            {
                user.IsBloked = applicationMyUser.IsBloked;
                if (ModelState.IsValid)
                {
                    log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " изменил пользователя " + applicationMyUser.Email + " с айди " + applicationMyUser.Id);
                    applicationMyUserRepository.Modified(user);
                    return RedirectToAction("Index");
                }
            }
            return View(applicationMyUser);
        }
            

        //    // GET: Journeys/Delete/5
        //    public ActionResult Delete(string id)
        //    {
        //        ApplicationMyUser user = applicationMyUserRepository.FindUser(id);
        //        if (user == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(user);
        //    }



        //// POST: Journeys/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{

        //    var userManager = new ApplicationUserManager(new UserStore<ApplicationMyUser>(new ApplicationDbContext()));

        //    ApplicationMyUser user = await userManager.FindByIdAsync(id);
        //    if (user != null)
        //    {
        //        user.LockoutEnabled = true;
        //        user.LockoutEndDateUtc = DateTime.Now.AddYears(100);
        //        await userManager.UpdateAsync(user);
        //        return RedirectToAction("Home/Index");
        //    }
        //    return HttpNotFound();
        //}
    }
}
