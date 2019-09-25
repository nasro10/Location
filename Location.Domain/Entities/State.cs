using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }

        public int CountryId { get; set; }
        [ForeignKey("CountryId ")]
        public virtual Country  Country { get; set; }
    }
}
