using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Domain.Models.Repositoryes.Interfases;
using TouristAgency.WebUI.Models;

namespace TouristAgency.Domain.Models.Repositoryes.Repositiry
{
    public class ApplicationMyUsersRepository : IRepositoryForUsers<ApplicationMyUser>, IDisposable
    {
        private ApplicationDbContext db;

        public ApplicationMyUsersRepository()
        {
            db = new ApplicationDbContext();
        }

        public void Add(ApplicationMyUser obj)
        {
            db.Users.Add(obj);
            Save();
        }

        public ApplicationMyUser FindUser(string id)
        {
            return db.Users.Find(id);
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

        public ApplicationMyUser Find(int? id)
        {
            return db.Users.Find(id);
        }

        public ApplicationMyUser FindById(string id)
        {
            return db.Users.Find(id);
        }

        public void Modified(ApplicationMyUser obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Remove(ApplicationMyUser obj)
        {
            db.Users.Remove(obj);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<ApplicationMyUser> ToList()
        {
            return db.Users.ToList();
        }
    }
}
