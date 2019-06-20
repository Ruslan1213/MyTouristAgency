using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Domain.Models.EfModels;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using TouristAgency.WebUI.Models;

namespace TouristAgency.Domain.Models.Repositoryes.Repositiry
{
    public class OrderStatusRepository : IRepository<OrderStatus>, IDisposable
    {
        private ApplicationDbContext db;

        public OrderStatusRepository()
        {
            db = new ApplicationDbContext();
        }

        public OrderStatus Find(int? id)
        {
            return db.OrderStatuses.Find(id);
        }

        public void Add(OrderStatus obj)
        {
            db.OrderStatuses.Add(obj);
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
        
        public void Modified(OrderStatus obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Remove(OrderStatus obj)
        {
            db.OrderStatuses.Remove(obj);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<OrderStatus> ToList()
        {
            return db.OrderStatuses.ToList();
        }
    }
}
