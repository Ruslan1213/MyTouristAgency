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
    public class HotelTypeRepository : IRepository<HotelsType>, IDisposable
    {
        private ApplicationDbContext db;

        public HotelTypeRepository()
        {
            db = new ApplicationDbContext();
        }

        public HotelsType Find(string id)
        {
            return db.HotelsTypes.Find(id);
        }

        public void Add(HotelsType obj)
        {
            db.HotelsTypes.Add(obj);
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

        public HotelsType Find(int? id)
        {
            return db.HotelsTypes.Find(id);
        }

        public void Modified(HotelsType obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Remove(HotelsType obj)
        {
            db.HotelsTypes.Remove(obj);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<HotelsType> ToList()
        {
            return db.HotelsTypes.ToList();
        }
    }
}
