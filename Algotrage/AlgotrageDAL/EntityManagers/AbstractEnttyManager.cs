using AlgotrageDAL.Context;
using AlgotrageDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.EntityManagers
{
    public class AbstractEnttyManager<T> where T : BaseEntity
    {
        public virtual List<T> GetAll()
        {
            using (var db = new AlgotrageContext())
            {
                return db.Set<T>().ToList();
            }
        }

        public virtual T GetById(int id)
        {
            using (var db = new AlgotrageContext())
            {
                return db.Set<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public virtual void Add(T t)
        {
            using (var db = new AlgotrageContext())
            {
                var entry = db.Entry(t);
                entry.State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public virtual void AddRange(List<T> ts)
        {
            using (var db = new AlgotrageContext())
            {
                db.Set<T>().AddRange(ts);
                db.SaveChanges();
            }
        }

        public virtual void Update(T t)
        {
            using (var db = new AlgotrageContext())
            {
                var entry = db.Entry(t);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public virtual void Remove(T t)
        {
            using (var db = new AlgotrageContext())
            {
                var entry = db.Entry(t);
                entry.State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
