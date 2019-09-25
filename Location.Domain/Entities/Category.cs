using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required, DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Category Description")]
        public string Description { get; set; }
        [DataType(DataType.ImageUrl), Display(Name = "Category Image")]
        public string Picture { get; set; }
        public Boolean Active { get; set; }

        public ICollection<SubCategory> subCategories { get; set; }
       

    }
}
