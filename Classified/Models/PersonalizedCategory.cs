using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class PersonalizedCategory
    {

        [Required]
        public int PersonalizedCategoryID { get; set; } // identic like ID of Category which we specify
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }

        public string State { get; set; }

        public int UserID { get; set;}
        public virtual User User { get; set; } // PersoanlizedCategory belongs to Concrete User
        public virtual ICollection<PCL> PCLs { get; set; } 

    }
}