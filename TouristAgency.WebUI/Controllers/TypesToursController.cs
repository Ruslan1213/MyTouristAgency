using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TouristAgency.Domain.Models.EfModels;
using TouristAgency.WebUI.Models;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using Ninject;
using TouristAgency.Domain.Models.Repositoryes.Repositiry;
using PagedList;

namespace TouristAgency.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class TypesToursController : Controller
    {
        private IRepository<TypesTour> db;

        public TypesToursController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<TypesTour>>().To<TypesTourRepositiry>();
            db = ninjectKernel.Get<IRepository<TypesTour>>();
        }

        public TypesToursController(IRepository<TypesTour> _orderStatusRepository)
        {
            db = _orderStatusRepository;
        }
        // GET: TypesTours
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: TypesTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesTour typesTour = db.Find(id);
            if (typesTour == null)
            {
                return HttpNotFound();
            }
            return View(typesTour);
        }

        // GET: TypesTours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypesTours/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTypeTour,TypeTour")] TypesTour typesTour)
        {
            if (ModelState.IsValid)
            {
                db.Add(typesTour);
                return RedirectToAction("Index");
            }

            return View(typesTour);
        }

        // GET: TypesTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesTour typesTour = db.Find(id);
            if (typesTour == null)
            {
                return HttpNotFound();
            }
            return View(typesTour);
        }

        // POST: TypesTours/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTypeTour,TypeTour")] TypesTour typesTour)
        {
            if (ModelState.IsValid)
            {
                db.Modified(typesTour);
                return RedirectToAction("Index");
            }
            return View(typesTour);
        }
    }
}
