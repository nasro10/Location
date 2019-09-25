using Location.Domain.Entities;
using Location.Service.Interfaces;
using LocationVoiture.Data.Infrastructures;
using LocationVoiture.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Service.Service
{
    public class ServiceInvoice : Service<Invoice>, IServiceInvoice
    {
        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public ServiceInvoice()
           : base(ut)
        {



        }

        public double CoutTotalRent(Booking b)
        {
            double sum = 0;
            if (b.RentType.Equals("1"))
            {
                 sum = b.RentNumber * b.Car.PricePerHour ;
            }
            else if (b.RentType.Equals("2"))
            {
                sum = b.RentNumber * b.Car.PricePerDay ;
            }
            else if (b.RentType.Equals("3"))
            {
                sum = b.RentNumber * b.Car.PricePerMonth ;
            }



            return sum;
        }
    }
}
