using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Domain.Entities
{
    public enum ClaimState { New,Open,Closed}
    public class Claims
    {
        public int ClaimID { get; set; }      
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ClaimDate { get; set; }
        public string TypeClaim { get; set; }
        public ClaimState state { get; set; }
        public int Phone { get; set; }
        public string UserN { get; set; }
        public virtual User User { get; set; }
    }
}
