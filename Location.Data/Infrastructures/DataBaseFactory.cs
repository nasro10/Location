using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationVoiture.Data.Infrastructures
{
   public class DataBaseFactory : Disposable, IDataBaseFactory
    {
        //fournir une instance de contexte
        private LocationContext ctx;

        public LocationContext DataContext
        {
            get { return ctx; }

        }

        public DataBaseFactory()
        {
            ctx = new LocationContext();
        }

        protected override void DisposeCore()
        {
            if (DataContext != null)

                DataContext.Dispose();
        }
    }
}
