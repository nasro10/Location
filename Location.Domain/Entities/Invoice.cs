using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
   public enum InvoiceState {Accepted,Refused,Returned }
   public class Invoice
    {
        public int InvoiceID { get; set; }
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        public float InvoicePrice { get; set; }
       
        public InvoiceState invoiceState { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
