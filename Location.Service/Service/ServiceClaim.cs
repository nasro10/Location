﻿using Location.Domain.Entities;
using LocationVoiture.Data.Infrastructures;
using LocationVoiture.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Service
{
    public class ServiceClaim : Service<Claims>, IServiceClaim
    {
        private static IDataBaseFactory dbf = new DataBaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public ServiceClaim()
           : base(ut)
        {



        }
    }
}
