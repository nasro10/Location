using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Areas.Admin.Models
{
    public class SubCategoryViewModel
    {
        public int SubCategoryID { get; set; }
        [Required, DisplayName("SubCategory Name")]
        public string Name { get; set; }
        [DataType(DataType.ImageUrl), Display(Name = "SubCategory Image")]
        public string Picture { get; set; }
        public float Subscription { get; set; }
        public bool Active { get; set; }

        [Display(Name = "Category ")]
        public int CategoryID { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public string CategoryName { get; set; } //pour l'affichage
    }
}