using Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Models
{
    public class CarViewModel
    {
        public int CarID { get; set; }
        [Required(ErrorMessage = "Serial Number is required")]
        public int SerialNumber { get; set; }
        [Display(Name = "Description")]
        public string Note { get; set; }
        public Fuel Fuel { get; set; }
        //Price Part

        public int LoanDuration { get; set; }

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

        [Display(Name = "CreationYear ")]
        public int CreationYearID { get; set; }
        [Display(Name = "TypeCar")]
        public int TypeCarID { get; set; }
        [Display(Name = "SubCategory ")]
        public int SubCatgoryID { get; set; }
        [Display(Name = "CarModel")]
        public int CarModelID { get; set; }


        public virtual IEnumerable<SelectListItem> TypeCars { get; set; }
        public virtual IEnumerable<SelectListItem> SubCategories { get; set; }
        public virtual IEnumerable<SelectListItem> CarModels { get; set; }
        public virtual IEnumerable<SelectListItem> CreationYear { get; set; }

        public string TypeCarname { get; set; } //pour l'affichage
        public string SubcategoryName { get; set; } //pour l'affichage
        public string CarModelName { get; set; } //pour l'affichage
        public string CreationYearName { get; set; } //pour l'affichage
    }
}