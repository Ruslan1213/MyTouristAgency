using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TouristAgency.Domain.Models.EfModels;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using TouristAgency.WebUI.Models;

namespace TouristAgency.Domain.Models.Repositoryes.Repositiry
{
    public class OrderRepository : IRepository<Order>, IDisposable
    {
        private ApplicationDbContext db;

        public OrderRepository()
        {
            db = new ApplicationDbContext();
        }

        public Order Find(int? id)
        {
            return db.Orders.Find(id);
        }

        public void Add(Order obj)
        {
            db.Orders.Add(obj);
            Save();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Modified(Order obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Remove(Order obj)
        {
            db.Orders.Remove(obj);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Order> ToList()
        {
            var orders = db.Orders.Include(o => o.Journey);
            return orders.ToList();
        }
    }
}
