using Location.Domain.Entities;
using Location.Service.Interfaces;
using LocationVoiture.Data.Infrastructures;
using LocationVoiture.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Location.Service.Service
{
    public class ServiceCar : Service<Car>, IServiceCar
    {
        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public ServiceCar()
           : base(ut)
        {



        }
       
    }
}
