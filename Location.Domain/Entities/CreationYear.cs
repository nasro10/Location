using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class CreationYear
    {
        public int CreationYearID { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
