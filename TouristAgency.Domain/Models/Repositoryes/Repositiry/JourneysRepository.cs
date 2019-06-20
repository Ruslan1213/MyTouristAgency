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
    public class JourneysRepository: IRepository<Journey>, IDisposable
    {
        private ApplicationDbContext db;

        public JourneysRepository()
        {
            db = new ApplicationDbContext();
        }

        public void Add(Journey obj)
        {
            db.Journeys.Add(obj);
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

        public Journey Find(int? id)
        {
            return db.Journeys.Find(id);
        }

        public void Modified(Journey obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Remove(Journey obj)
        {
            db.Journeys.Remove(obj);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Journey> ToList()
        {
            return GetJourneysForView().ToList();
        }

        private IQueryable<Journey> GetHotJourneys()
        {
            return db.Journeys.Where(j => j.StartedAmount - j.QuantitySold > 0 && j.ExpirstionDate > DateTime.Now && j.StartedDate > DateTime.Now && j.IsLastMinuteTrip == true && j.Tour.IsDeleted == false && j.IsDeleted==false).Include(j => j.Tour);
        }

        private IQueryable<Journey> GetNotHotJourneys()
        {
            return db.Journeys.Where(j => j.StartedAmount - j.QuantitySold > 0 && j.ExpirstionDate > DateTime.Now && j.StartedDate>DateTime.Now && j.IsLastMinuteTrip == false && j.Tour.IsDeleted == false && j.IsDeleted == false).Include(j => j.Tour);
        }
        private IQueryable<Journey> GetJourneysForView()
        {
            return GetHotJourneys().Concat(GetNotHotJourneys());
        }
    }
}
