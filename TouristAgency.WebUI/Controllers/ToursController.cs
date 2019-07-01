using log4net;
using Ninject;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TouristAgency.Domain.Models.EfModels;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using TouristAgency.Domain.Models.Repositoryes.Repositiry;
using TouristAgency.Domain.Validation;
using TouristAgency.WebUI.Models;

namespace TouristAgency.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ToursController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(ToursController));
        private IRepository<Tour> db;
        private IRepository<TypesTour> dbTypesTour;
        private IRepository<HotelsType> dbHotelsType;

        public ToursController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<TypesTour>>().To<TypesTourRepositiry>();
            ninjectKernel.Bind<IRepository<HotelsType>>().To<HotelTypeRepository>();
            ninjectKernel.Bind<IRepository<Tour>>().To<TourRepository>();
            db = ninjectKernel.Get<IRepository<Tour>>();
            dbTypesTour = ninjectKernel.Get<IRepository<TypesTour>>();
            dbHotelsType = ninjectKernel.Get<IRepository<HotelsType>>();
        }

        // GET: Tours
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Tours/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.IdHotelsType = new SelectList(dbHotelsType.ToList(), "IdHotelsType", "HotelType");
            ViewBag.IdTypeTour = new SelectList(dbTypesTour.ToList(), "IdTypeTour", "TypeTour");
            return View();
        }

        // POST: Tours/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTour,IdTypeTour,IdHotelsType,Name,Discription,Price,StartNumberOfPeople,EndNumberOfPeople")] Tour tour)
        {
            string error = "";
            TourValidation tourValidation = new TourValidation(tour);
            error = tourValidation.IsValidationSuccessful().ErrorValidation;
            if (ModelState.IsValid && tourValidation.IsValidationSuccessful().Validation)
            {
                tour.IsDeleted = false;
                log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " добавил тур " + tour.IdTour + " с названием " + tour.Name);
                db.Add(tour);
                return RedirectToAction("Index");
            }

            ViewBag.IdHotelsType = new SelectList(dbHotelsType.ToList(), "IdHotelsType", "HotelType", tour.IdHotelsType);
            ViewBag.IdTypeTour = new SelectList(dbTypesTour.ToList(), "IdTypeTour", "TypeTour", tour.IdTypeTour);

            ViewBag.error = error;
            return View(tour);
        }

        // GET: Tours/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHotelsType = new SelectList(dbHotelsType.ToList(), "IdHotelsType", "HotelType", tour.IdHotelsType);
            ViewBag.IdTypeTour = new SelectList(dbTypesTour.ToList(), "IdTypeTour", "TypeTour", tour.IdTypeTour);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "IdTour,IdTypeTour,IdHotelsType,Name,Discription,Price,StartNumberOfPeople,EndNumberOfPeople,IsDeleted")] Tour tour)
        {
            string error = "";
            TourValidation tourValidation = new TourValidation(tour);
            error = tourValidation.IsValidationSuccessful().ErrorValidation;
            if (ModelState.IsValid && tourValidation.IsValidationSuccessful().Validation)
            {
                db.Modified(tour);
                log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " изменил тур " + tour.IdTour + " с названием " + tour.Name);

                return RedirectToAction("Index");
            }
            ViewBag.IdHotelsType = new SelectList(dbHotelsType.ToList(), "IdHotelsType", "HotelType", tour.IdHotelsType);
            ViewBag.IdTypeTour = new SelectList(dbTypesTour.ToList(), "IdTypeTour", "TypeTour", tour.IdTypeTour);

            ViewBag.error = error;
            return View(tour);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Find(id);
            tour.IsDeleted = true;
            log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " удалил тур " + tour.IdTour + " с названием " + tour.Name);
            db.Modified(tour);
            return RedirectToAction("Index");
        }
    }
}
