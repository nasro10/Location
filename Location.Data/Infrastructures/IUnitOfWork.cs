using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Data.Infrastructures
{
    //gérer les transactions avec la base
    public interface IUnitOfWork : IDisposable
    {


        void Commit();
        IRepositoryBase<T> GetRepositoryBase<T>() where T : class;

    }
}
