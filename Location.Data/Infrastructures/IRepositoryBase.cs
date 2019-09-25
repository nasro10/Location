using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Data.Infrastructures
{
    public interface IRepositoryBase<T> where T : class //interface fortement typé à un type generique T
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

    }
}
