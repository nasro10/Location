using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class CarModel
    {
        public int CarModelID { get; set; }
        [Required]
        public string Name { get; set; }
        public Boolean Active { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
