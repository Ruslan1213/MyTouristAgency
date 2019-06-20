using log4net;
using Microsoft.AspNet.Identity;
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
    public class OrdersController : Controller
    {
        //private IRepositoryForUsers<ApplicationMyUser> applicationMyUserRepository;
        //private IRepository<OrderStatus> orderStatusRepository;
        //private IRepository<Journey> journeyRepository;
        //private IRepository<Order> orderRepository;
        ApplicationDbContext db;

        ILog log = log4net.LogManager.GetLogger(typeof(OrdersController));
        public OrdersController()
        {
            db = new ApplicationDbContext();
            //IKernel ninjectKernel = new StandardKernel();

            //ninjectKernel.Bind<IRepositoryForUsers<ApplicationMyUser>>().To<ApplicationMyUsersRepository>();
            //applicationMyUserRepository = ninjectKernel.Get<IRepositoryForUsers<ApplicationMyUser>>();

            //ninjectKernel.Bind<IRepository<OrderStatus>>().To<OrderStatusRepository>();
            //orderStatusRepository = ninjectKernel.Get<IRepository<OrderStatus>>();

            //ninjectKernel.Bind<IRepository<Journey>>().To<JourneysRepository>();
            //journeyRepository= ninjectKernel.Get<IRepository<Journey>>();

            //ninjectKernel.Bind<IRepository<Order>>().To<OrderRepository>();
            //orderRepository = ninjectKernel.Get<IRepository<Order>>();
        }

        // GET: Orders
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.Orders.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create

        [HttpGet]
        [Authorize]
        public ActionResult MyCreate(int? Idjourney)
        {
            if (Idjourney == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Order order = new Order();
            order.Discount = 0;
            ApplicationMyUser myUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            order.User = myUser;
            
            order.OrderStatus = db.OrderStatuses.Find(1);
            order.Journey = db.Journeys.Find(Idjourney);
            return Redirect("Home/Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MyCreate(Journey journey)
        //{
        //    Order order = new Order();
        //    if (ModelState.IsValid)
        //    {
        //        order.Discount = 0;
        //        ApplicationMyUser myUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
        //        order.User = myUser;
        //        order.OrderStatus = db.OrderStatuses.Find(1);
        //        order.Journey = db.Journeys.Find(journey.IdJourney);
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Home");
        //    }

        //    ViewBag.IdJourney = new SelectList(db.Journeys, "IdJourney", "IdJourney", order.IdJourney);
        //    return View("Index");
        //}

        [HttpGet]
        [Authorize]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.IdJourney = new SelectList(db.Journeys.ToList(), "IdJourney", "IdJourney");
                Order order = new Order();
                Journey journey = db.Journeys.Find(id);
                order.Journey = journey;
                order.IdJourney = id;
                ApplicationMyUser myUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
                order.User = myUser;
                return View(order);
            }
            return Redirect("Home/Index");
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdOrder,IdJourney,IdOrderStatus,Discount,User_Id,CountOfJourneys")] Order order)
        {
            Journey journey = db.Journeys.Find(order.IdJourney);
            order.Journey = journey;
            if (ModelState.IsValid)
            {
                OrderStatus orderStatus = db.OrderStatuses.Find(1);
                order.Discount = 0;
                ApplicationMyUser myUser = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
                order.User = myUser;
                order.Journey = journey;
                int? countJourneys = journey.StartedAmount - journey.QuantitySold;
                if (countJourneys != null)
                {
                    if (order.CountOfJourneys > 0 && order.CountOfJourneys <= countJourneys)
                    {
                        EditJourneys(journey, (int)order.CountOfJourneys);
                        OrderStatus status = db.OrderStatuses.Find(1);
                        order.OrderStatus = status;
                        order.OrderStatus_IdOrder = 1;
                        db.Orders.Add(order);
                        //orderRepository.Add(order);
                        log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " создал заказ "+order.IdOrder);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = "Колличество запрашиваемых путевок выдать не возможно так как осталось " + countJourneys.ToString();
                        log.Info(" Пользователь " + User.Identity.Name + " c айпи " + Request.UserHostAddress + " пытался создать заказ ");
                        return View(order);
                    }

                }
                return View(order);
            }

            ViewBag.IdJourney = new SelectList(db.Journeys.ToList(), "IdJourney", "IdJourney", order.IdJourney);
            return View(order);
        }




        private void EditJourneys(Journey journey,int countJourneys)
        {
            journey.QuantitySold = journey.QuantitySold + countJourneys;
            db.Entry(journey).State = EntityState.Modified;
        }






        //bool IsOtmena = false;
        // GET: Orders/Edit/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            //if (order.OrderStatus.OrdersStatus != "Отменен")
            //    IsOtmena = true;
            ViewBag.IdJourney = new SelectList(db.Journeys.ToList(), "IdJourney", "IdJourney", order.IdJourney);
            ViewBag.Status = new SelectList(db.OrderStatuses.ToList(), "IdOrder", "OrdersStatus", order.OrderStatus_IdOrder);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные  [Bind(Include = "IdOrder,IdJourney,IdOrder,Discount")]
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit([Bind(Include = "IdOrder,IdJourney,Discount,CountOfJourneys,OrderStatus_IdOrder")]Order order,int OrderStatus_IdOrder)
        {
            


            Order editOrder = db.Orders.Find(order.IdOrder);
            editOrder.Discount = order.Discount;
            editOrder.OrderStatus = order.OrderStatus;
            if (ModelState.IsValid)
            {
                //OrderStatus orderStatus = db.OrderStatuses.Where(x=>x.OrdersStatus==OrderStatus_IdOrder).FirstOrDefault();
                OrderStatus orderStatus = db.OrderStatuses.Find(OrderStatus_IdOrder);
                editOrder.OrderStatus = orderStatus;
                editOrder.OrderStatus_IdOrder = orderStatus.IdOrder;
                //if (editOrder.OrderStatus.OrdersStatus == "Отменен" && IsOtmena)
                //{
                //    Journey journey = new Journey();
                //    journey = editOrder.Journey;
                //    journey.QuantitySold -= 1;
                //    db.Entry(journey).State = EntityState.Modified;
                //}
                db.Entry(editOrder).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            ViewBag.IdJourney = new SelectList(db.Journeys.ToList(), "IdJourney", "IdJourney", order.IdJourney);
            ViewBag.IdOrder = new SelectList(db.OrderStatuses.ToList(), "IdOrder", "OrdersStatus", order.OrderStatus_IdOrder);
            return View(order);
        }


        //// POST: Orders1/Edit/5
        //// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        //// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IdOrder,IdJourney,Discount,CountOfJourneys,OrderStatus_IdOrder")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdJourney = new SelectList(db.Journeys, "IdJourney", "IdJourney", order.IdJourney);
        //    return View(order);
        //}

        // GET: Orders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        
    }
}
