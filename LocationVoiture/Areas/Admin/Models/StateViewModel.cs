using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Models
{
    public class StateViewModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }


        [Display(Name = "Country ")]
        public int CountryID { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public string Country { get; set; } //pour l'affichage
    }
}