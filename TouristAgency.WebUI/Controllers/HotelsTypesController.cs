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
using TouristAgency.WebUI.Models;

namespace TouristAgency.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class HotelsTypesController : Controller
    {
        private IRepository<HotelsType> hotelTypeRepository;

        public HotelsTypesController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<HotelsType>>().To<HotelTypeRepository>();
            hotelTypeRepository = ninjectKernel.Get<IRepository<HotelsType>>();
        }

        public HotelsTypesController(IRepository<HotelsType> _hotelTypeRepository)
        {
            hotelTypeRepository = _hotelTypeRepository;
        }

        // GET: HotelsTypes
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(hotelTypeRepository.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: HotelsTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelsType hotelsType = hotelTypeRepository.Find(id);
            if (hotelsType == null)
            {
                return HttpNotFound();
            }
            return View(hotelsType);
        }

        // GET: HotelsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HotelsTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHotelsType,HotelType")] HotelsType hotelsType)
        {
            if (ModelState.IsValid)
            {
                hotelTypeRepository.Add(hotelsType);
                return RedirectToAction("Index");
            }

            return View(hotelsType);
        }

        // GET: HotelsTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelsType hotelsType = hotelTypeRepository.Find(id);
            if (hotelsType == null)
            {
                return HttpNotFound();
            }
            return View(hotelsType);
        }

        // POST: HotelsTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHotelsType,HotelType")] HotelsType hotelsType)
        {
            if (ModelState.IsValid)
            {
                hotelTypeRepository.Modified(hotelsType);
                return RedirectToAction("Index");
            }
            return View(hotelsType);
        }

        // GET: HotelsTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelsType hotelsType = hotelTypeRepository.Find(id);
            if (hotelsType == null)
            {
                return HttpNotFound();
            }
            return View(hotelsType);
        }

        // POST: HotelsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelsType hotelsType = hotelTypeRepository.Find(id);
            hotelTypeRepository.Remove(hotelsType);
            return RedirectToAction("Index");
        }
    }
}
