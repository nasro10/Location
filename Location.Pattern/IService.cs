using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Pattern
{
    public interface IService<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);

        void Update(T entity);

        T GetById(int id);

        T GetById(string id);
        T Get(Expression<Func<T, bool>> condition);

        //GetMany():sans param, GetMany(condition): avec param
        IEnumerable<T> GetMany(Expression<Func<T, bool>> condition = null,
            Expression<Func<T, bool>> orderBy = null);

        void Delete(Expression<Func<T, bool>> condition);

        void Commit();

        void Dispose();

    }
}
