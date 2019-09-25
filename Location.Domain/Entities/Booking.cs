using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public enum BookingState { Sent, Approved, Refused,DeliveredToClient,FinishedbyClient,ConfirmedByOwner, ConfirmedByClient }
    public class Booking
    {
    
       
        public int BookingID { get; set; }

        public BookingState bookingState { get; set; }
        public int RentNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartingFrom { get; set; }

        public string RentType { get; set; }

        //State Dates
        [DataType(DataType.Date)]
        public DateTime BookingRequestDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingApproveDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingRefuseDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingDeliveredToClientDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingFinishedbyClientDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingConfirmedByOwnerDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingConfirmedByClientDate { get; set; }

        public string ActivationCode { get; set; }

        public bool Driver { get; set; }
        public bool Delivery { get; set; }
        public string Note { get; set; }


        public int? CarId { get; set; }


        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual Invoice Invoice { get; set; }


    }
}
