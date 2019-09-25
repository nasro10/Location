using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Data.Configurations
{
    public class DateTime2Convention1: Convention
    {
            public DateTime2Convention1()
            {
                this.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            }
        
    }
}
