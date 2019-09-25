using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Service.Interfaces
{
    public interface IServiceRating
    {
        void addRate(Rate r);
        double avgRate(Car c);

        void commit();

       
    }
}
