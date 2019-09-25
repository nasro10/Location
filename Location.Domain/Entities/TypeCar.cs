using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class TypeCar
    {
        [Key]
        public int TypeID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.ImageUrl), Display(Name = "Logo")]
        public string Picture { get; set; }
        public Boolean Active { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
