using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Data.Infrastructures
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        LocationContext ctx = null;
        readonly private IDbSet<T> dbset;



        public RepositoryBase(LocationContext ctx)
        {
            this.ctx = ctx;
            dbset = ctx.Set<T>(); //pour remplir le dbset à partir du contexte
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> condition)
        {

            foreach (T entity in dbset.Where(condition))
                dbset.Remove(entity);

        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual T Get(Expression<Func<T, bool>> condition)
        {
            return dbset.Where(condition).FirstOrDefault();
        }

        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

     


        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy = null)
        {
            IQueryable<T> query = dbset;
            if (condition != null)
                query = query.Where(condition);
            if (orderBy != null)
                query = query.OrderBy(orderBy);
            return dbset.AsEnumerable();
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            ctx.Entry(entity).State = EntityState.Modified;
            
        }
    }
}
