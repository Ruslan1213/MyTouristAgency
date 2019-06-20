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
    public class TourRepository:IRepository<Tour>,IDisposable
    {
        private ApplicationDbContext db;

        public TourRepository()
        {
            db = new ApplicationDbContext();
        }

        public void Add(Tour obj)
        {
            db.Tours.Add(obj);
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

        public Tour Find(int? id)
        {
            return db.Tours.Find(id);
        }

        public void Modified(Tour obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Remove(Tour obj)
        {
            db.Tours.Remove(obj);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Tour> ToList()
        {
            return db.Tours.Include(t => t.HotelsType).Include(t => t.TypesTour).ToList();
        }
    }
}
