using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class Notification
    {
        public int NotificationID { get; set; }
        [DataType(DataType.Date)]
        public DateTime SendDate { get; set; }
        public string Description { get; set; }


        public virtual User User { get; set; }
    }
}
