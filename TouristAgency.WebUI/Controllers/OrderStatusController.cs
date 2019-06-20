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
    public class OrderStatusController : Controller
    {
        private IRepository<OrderStatus> orderStatusRepository;

        public OrderStatusController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository<OrderStatus>>().To<OrderStatusRepository>();
            orderStatusRepository = ninjectKernel.Get<IRepository<OrderStatus>>();
        }

        public OrderStatusController(IRepository<OrderStatus> _orderStatusRepository)
        {
            orderStatusRepository = _orderStatusRepository;
        }

        // GET: OrderStatus
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orderStatusRepository.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: OrderStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = orderStatusRepository.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderStatus/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrder,OrdersStatus")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                orderStatusRepository.Add(orderStatus);
                return RedirectToAction("Index");
            }

            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = orderStatusRepository.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // POST: OrderStatus/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrder,OrdersStatus")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                orderStatusRepository.Modified(orderStatus);
                return RedirectToAction("Index");
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = orderStatusRepository.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderStatus orderStatus = orderStatusRepository.Find(id);
            orderStatusRepository.Remove(orderStatus);
            return RedirectToAction("Index");
        }
    }
}
