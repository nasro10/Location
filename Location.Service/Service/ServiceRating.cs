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
    public class ServiceRating : Service<Rate>, IServiceRating
    {
        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public ServiceRating()
           : base(ut)
        {



        }

        public void addRate(Rate r)
        {
            Add(r);
        }

        public double avgRate(Car c)
        {
            double Moyenne;
            IEnumerable<Rate> liste = new List<Rate>();
            liste = GetMany().Where(r => r.CarId == c.CarID).ToList();
            int f = liste.Sum(r => r.rate1);
            if (liste.Count() == 0)
            {
                Moyenne = f / 1;
            }
            else
            {
                Moyenne = f / liste.Count();
            }


            return Moyenne;
        }


        public double avgRateUser(User u)
        {
            double Moyenne;
            IEnumerable<Rate> liste = new List<Rate>();
            liste = GetMany().Where(r => r.UserId == u.UserID).ToList();
            int f = liste.Sum(r => r.Number);
            if (liste.Count() == 0)
            {
                Moyenne = f / 1;
            }
            else
            {
                Moyenne = f / liste.Count();
            }


            return Moyenne;
        }

        public void commit()
        {
            Commit();
        }

        
     

    }
}
