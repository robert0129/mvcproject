using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvcproject.Models.Repository
{
    public class CustomersRepositoryProvider : ICustomersRepository, IDisposable
    {
        private customerEntities db;

        public CustomersRepositoryProvider(customerEntities customerEntities)
        {
            this.db = customerEntities;
        }

        public void Create(客戶資料 instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            } else
            {
                db.客戶資料.Add(instance);
                this.SaveChanges();
            }
        }

        public void Delete(客戶資料 instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            } else
            {
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public 客戶資料 Get(int CustomerId)
        {
            return db.客戶資料.FirstOrDefault(x => x.Id == CustomerId);
        }

        public IEnumerable<客戶資料> GetAll()
        {
            return db.客戶資料.OrderBy(x => x.Id);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Update(客戶資料 instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            } else
            {
                db.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}