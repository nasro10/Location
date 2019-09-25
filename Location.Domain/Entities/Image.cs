using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Location.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }
     
        public string FileName { get; set; }
        public string Extension { get; set; }
      

        public int carId { get; set; }
        [ForeignKey("carId")]
        public virtual Car Car { get; set; }

    }
}
