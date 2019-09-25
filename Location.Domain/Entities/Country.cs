using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
   public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
