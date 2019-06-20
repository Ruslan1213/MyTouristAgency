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
    public class TypesTourRepositiry : IRepository<TypesTour>, IDisposable
    {
        private ApplicationDbContext db;

        public TypesTourRepositiry()
        {
            db = new ApplicationDbContext();
        }

        public void Add(TypesTour obj)
        {
            db.TypesTours.Add(obj);
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

        public TypesTour Find(int? id)
        {
            return db.TypesTours.Find(id);
        }

        public void Modified(TypesTour obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Remove(TypesTour obj)
        {
            db.TypesTours.Remove(obj);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<TypesTour> ToList()
        {
            return db.TypesTours.ToList();
        }
    }
}
