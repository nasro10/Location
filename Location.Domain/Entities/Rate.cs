using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class Rate
    {
       
        [Key]
        public int RateID { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public int rate1 { get; set; }
        public int? CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")] 
        public virtual User User { get; set; }

    }
}
