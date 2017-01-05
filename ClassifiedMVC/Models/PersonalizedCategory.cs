using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class PersonalizedCategory
    {

        [Required]
        public int PersonalizedCategoryID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public string UserID { get; set; }
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }
        public string State { get; set; }

        public string Path { get; set; }

        


        public virtual User User { get; set; } // PersoanlizedCategory belongs to Concrete User
        public virtual Category Category { get; set; }
        public virtual ICollection<PCL> PCLs { get; set; } 

    }
}