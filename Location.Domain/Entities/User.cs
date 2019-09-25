using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public enum Gender { Male,Female}
    public class User 
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }

        //[RegularExpression(@"^[0-9]{8,11}$", ErrorMessage = "Not a valid Cin, must be between 8 and 11 digits")]
        public int CIN { get; set; }

        [RegularExpression(@"^[0-9]{8,11}$", ErrorMessage = "Not a valid phone number, must be between 8 and 11 digits")]
        public int phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime SignUpDate { get; set; }
        [DataType(DataType.ImageUrl), Display(Name = "Profile Picture")]
        public string Picture { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
      
        
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The Password and confirm password are not the same")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.ImageUrl), Display(Name = "Cin Picture")]
        public string CinPicture { get; set; }

        [DataType(DataType.ImageUrl), Display(Name = "Working Licence")]
        public string WorkingLicencePicture { get; set; }

        [DataType(DataType.ImageUrl), Display(Name = "Work Report")]
        public string WorkReport { get; set; }
        public int CreditCard { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }


        public virtual State State { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Claims> Claims { get; set; }

        public virtual ICollection<Booking> Reservations { get; set; }

    

    }

 

}