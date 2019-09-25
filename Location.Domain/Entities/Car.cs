using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public enum Gear { Normal, Automatic }
    public enum Fuel{Essence,Diesel }
    public class Car
    {

        public int CarID { get; set; }
        [Required(ErrorMessage = "Serial Number is required")]
        public int SerialNumber { get; set; }
        [Display(Name = "Description")]
        public string Note { get; set; }
        public Fuel Fuel { get; set; }
        //Price Part
        
        public string MinPriceForRent { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public double PricePerHour { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public double PricePerDay { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public double PricePerMonth { get; set; }

        public int seatNumber { get; set; }

        public Gear Gear { get; set; }
        public string Comment { get; set; }
        public bool Available { get; set; }



        public bool Active { get; set; }

        //  public virtual Responsable Owner { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

       
        public int? TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual TypeCar TypeCar { get; set; }
    
        public int? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        public int? CarModelId { get; set; }
        [ForeignKey("CarModelId")]
        public virtual CarModel CarModel { get; set; }

        public int? CreationYearId { get; set; }
        [ForeignKey("CreationYearId")]
        public virtual CreationYear CreationYear { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Image> Images { get; set; }

    }
}
