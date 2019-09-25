using LocationVoiture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Data.Infrastructures
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private LocationContext ctx;
        private IDataBaseFactory factory;
        public UnitOfWork(IDataBaseFactory factory)
        {
            this.factory = factory;
            ctx = factory.DataContext;
        }
        public void Commit()
        {
            ctx.SaveChanges();
        }

        public void Dispose()
        {
            factory.Dispose();
        }

        public IRepositoryBase<T> GetRepositoryBase<T>() where T : class
        {
            return new RepositoryBase<T>(ctx);
        }
    }
}
