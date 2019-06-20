using Ninject;
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
using TouristAgency.WebUI.Models;
using PagedList.Mvc;
using PagedList;
using log4net;

namespace TouristAgency.WebUI.Controllers
{
    public class JourneysController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(JourneysController));
        private IRepository<Journey> db;
        private IRepository<Tour> dbTour;

        public JourneysController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<Journey>>().To<JourneysRepository>();
            ninjectKernel.Bind<IRepository<Tour>>().To<TourRepository>();
            db = ninjectKernel.Get<IRepository<Journey>>();
            dbTour = ninjectKernel.Get<IRepository<Tour>>();

        }

        public JourneysController(IRepository<Journey> _db, IRepository<Tour> _dbTour)
        {
            db = _db;
            dbTour = _dbTour;
        }

        // GET: Journeys
        [Authorize]
        public ActionResult Index(int? page, string startPrice, string finalPrice, string sAmountOfPeople, string eAmountOfPeople, string typeTour, string hotelType)
        {
            if (startPrice!=null || startPrice != "")
                ViewBag.startPrice = startPrice;
            if (finalPrice != null || finalPrice !="")
                ViewBag.finalPrice = finalPrice;
            if (sAmountOfPeople != null || sAmountOfPeople!="")
                ViewBag.sAmountOfPeople = sAmountOfPeople;
            if (eAmountOfPeople != null)
                ViewBag.eAmountOfPeople = eAmountOfPeople;

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            string error = "";
            int sPrice = 0;
            int ePrice = 0;
            int peopleCount = 0;
            int EpeopleCount = 0;
            




            ViewBag.TypesTours = GetListTypeTours();
            ViewBag.HotelsType = GetListHotelsTypes();
            var searching = db.ToList();
            if (typeTour != "" && typeTour != null && typeTour != "---Please select ---")
            {
                searching = searching.Where(j => j.Tour.TypesTour.TypeTour == typeTour);
            }
            if (hotelType != "" && hotelType != null && hotelType != "---Please select ---")
            {
                searching = searching.Where(j => j.Tour.HotelsType.HotelType == hotelType);
            }
            try
            {
                sPrice = int.Parse(startPrice);
                ePrice = int.Parse(finalPrice);
                searching = searching.Where(j => (int)j.Tour.Price >= sPrice && (int)j.Tour.Price <= ePrice);
            }
            catch (Exception e)
            {

                error += "В поле цена введено дробное число или строка, введите целое число!";
                log.Error(e.Message + " Вызвана " + User.Identity.Name + " c айпи " + Request.UserHostAddress);
            }
            try
            {
                peopleCount = int.Parse(sAmountOfPeople);
                EpeopleCount = int.Parse(eAmountOfPeople);
                searching = searching.Where(j => (int)j.Tour.StartNumberOfPeople >= peopleCount && (int)j.Tour.EndNumberOfPeople <= EpeopleCount);
            }
            catch (Exception e)
            {
                error += "В поле колличество человек введены не коректные данные";
                log.Error(e.Message + " Вызвана " + User.Identity.Name + " c айпи " + Request.UserHostAddress);
            }
            return View(searching.ToPagedList(pageNumber, pageSize));
        }



        //[Authorize]
        //[HttpPost]
        //public ActionResult Index(int? page, string startPrice, string finalPrice, string sAmountOfPeople, string eAmountOfPeople, string typeTour, string hotelType)
        //{
        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);

        //    string error = "";
        //    int sPrice = 0;
        //    int ePrice = 0;
        //    int peopleCount = 0;
        //    int EpeopleCount = 0;

        //    ViewBag.startPrice = "100";
        //    ViewBag.finalPrice = finalPrice;
        //    ViewBag.sAmountOfPeople = sAmountOfPeople;
        //    ViewBag.eAmountOfPeople = eAmountOfPeople;




        //    ViewBag.TypesTours = GetListTypeTours();
        //    ViewBag.HotelsType = GetListHotelsTypes();
        //    var searching = db.ToList();
        //    if (typeTour != "" && typeTour != null && typeTour != "---Please select ---")
        //    {
        //        searching = searching.Where(j => j.Tour.TypesTour.TypeTour == typeTour);
        //    }
        //    if (hotelType != "" && hotelType != null && hotelType != "---Please select ---")
        //    {
        //        searching = searching.Where(j => j.Tour.HotelsType.HotelType == hotelType);
        //    }
        //    try
        //    {
        //        sPrice = int.Parse(startPrice);
        //        ePrice = int.Parse(finalPrice);
        //        searching = searching.Where(j => (int)j.Tour.Price >= sPrice && (int)j.Tour.Price <= ePrice);
        //    }
        //    catch (Exception e)
        //    {

        //        error += "В поле цена введено дробное число или строка, введите целое число!";
        //        log.Error(e.Message + " Вызвана " + User.Identity.Name + " c айпи " + Request.UserHostAddress);
        //    }
        //    try
        //    {
        //        peopleCount = int.Parse(sAmountOfPeople);
        //        EpeopleCount = int.Parse(eAmountOfPeople);
        //        searching = searching.Where(j => (int)j.Tour.StartNumberOfPeople >= peopleCount && (int)j.Tour.EndNumberOfPeople <= EpeopleCount);
        //    }
        //    catch (Exception e)
        //    {
        //        error += "В поле колличество человек введены не коректные данные";
        //        log.Error(e.Message + " Вызвана " + User.Identity.Name + " c айпи " + Request.UserHostAddress);
        //    }
        //    ViewBag.Erroe = error;
        //    return View(searching.ToList().ToPagedList(pageNumber, pageSize));
        //}







        [Authorize]
        // GET: Journeys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            return View(journey);
        }

        // GET: Journeys/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.IdTour = new SelectList(dbTour.ToList(), "IdTour", "Name");
            return View();
        }

        // POST: Journeys/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdJourney,IdTour,StartedDate,ExpirstionDate,StartedAmount,QuantitySold,IsLastMinuteTrip,IsDeleted")] Journey journey)
        {

            if (ModelState.IsValid && journey.StartedDate < journey.ExpirstionDate && journey.StartedDate > DateTime.Now && journey.DateDifference() < 2 && journey.StartedAmount < 2000)

            {
                log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " добавил путевку " + journey.IdJourney + " со стартовой датой " + journey.StartedDate);
                db.Add(journey);
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Поля дат введены не верно";
            ViewBag.IdTour = new SelectList(dbTour.ToList(), "IdTour", "Name", journey.IdTour);
            return View(journey);
        }

        // GET: Journeys/Edit/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTour = new SelectList(dbTour.ToList(), "IdTour", "Name", journey.IdTour);
            return View(journey);
        }

        // POST: Journeys/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdJourney,IdTour,StartedDate,ExpirstionDate,StartedAmount,QuantitySold,IsLastMinuteTrip,IsDeleted")] Journey journey)
        {
            if (ModelState.IsValid && journey.StartedDate < journey.ExpirstionDate && journey.StartedDate > DateTime.Now && journey.DateDifference() < 2 && journey.StartedAmount < 2000)
            {
                log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " изменил путевку " + journey.IdJourney + " со стартовой датой " + journey.StartedDate);
                db.Modified(journey);
                return RedirectToAction("Index");
            }
            if (journey.StartedDate < journey.ExpirstionDate || journey.StartedDate > DateTime.Now || journey.DateDifference() < 2)
                ViewBag.Error = "Поля дат введены не верно";

            ViewBag.IdTour = new SelectList(dbTour.ToList(), "IdTour", "Name", journey.IdTour);
            return View(journey);
        }

        // GET: Journeys/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            return View(journey);
        }

        // POST: Journeys/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Journey journey = db.Find(id);
            journey.IsDeleted = true;
            log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " удалил путевку " + journey.IdJourney + " со стартовой датой " + journey.StartedDate);
            db.Modified(journey);
            return RedirectToAction("Index");
        }


        ApplicationDbContext db1 = new ApplicationDbContext();
        private IEnumerable<SelectListItem> GetListTypeTours()
        {
            string str;
            List<TypesTour> typesTours = db1.TypesTours.ToList();
            foreach (var s in typesTours)
            {
                str = s.TypeTour;
                str = str.Replace(" ", "");
                s.TypeTour = str;
            }
            return
                from c in typesTours
                select new SelectListItem
                {
                    Text = c.TypeTour.Trim(),
                    Value = c.TypeTour
                };
        }

        private IEnumerable<SelectListItem> GetListHotelsTypes()
        {
            string str;
            List<HotelsType> hotelsType = db1.HotelsTypes.ToList();
            foreach (var s in hotelsType)
            {
                str = s.HotelType;
                str = str.Replace(" ", "");
                s.HotelType = str;
            }
            return
                from c in hotelsType
                select new SelectListItem
                {
                    Text = c.HotelType.Trim(),
                    Value = c.HotelType
                };
        }
    }
}
