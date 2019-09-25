using LocationVoiture.Data.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Pattern
{
    public class Service<T> : IService<T> where T : class
    {
        private IUnitOfWork UTK;

        public Service(IUnitOfWork UTK)
        {
            this.UTK = UTK;

        }
        public void Add(T entity)
        {
            UTK.GetRepositoryBase<T>().Add(entity);

        }



        public void Commit()
        {
            UTK.Commit();

        }

        public void Delete(Expression<Func<T, bool>> condition)
        {
            UTK.GetRepositoryBase<T>().Delete(condition);
        }

        public void Delete(T entity)
        {

            UTK.GetRepositoryBase<T>().Delete(entity);

        }

        public void Dispose()
        {
            UTK.Dispose();
        }

        public T Get(Expression<Func<T, bool>> condition)
        {
            return UTK.GetRepositoryBase<T>().Get(condition);
        }

        public T GetById(string id)
        {
            return UTK.GetRepositoryBase<T>().GetById(id);
        }

        public T GetById(int id)
        {
            return UTK.GetRepositoryBase<T>().GetById(id);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> condition = null, Expression<Func<T, bool>> orderBy = null)
        {
            return UTK.GetRepositoryBase<T>().GetMany(condition);

        }

        public void Update(T entity)
        {
            UTK.GetRepositoryBase<T>().Update(entity);
        }
    }
}
