using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Subscription { get; set; }
        [DataType(DataType.ImageUrl), Display(Name = "SubCategory Image")]
        public string Picture { get; set; }
        public Boolean Active { get; set; }
        
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId ")]
        public virtual Category Category { get; set; }
        public ICollection<Car> cars { get; set; }
    }
}
