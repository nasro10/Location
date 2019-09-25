using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Data.CustomConventions
{
    public class CodeConvention : Convention
    {
        public CodeConvention()
        {
            Properties<int>().Where(p => p.Name.EndsWith("ID")).Configure(p => p.IsKey());
        }
    }
}
