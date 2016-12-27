using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class PCL
    {
        [Required]
        public int PCLID { get; set; }

        [Required]
        public int PersonalizedCategoryID { get; set; }

        [Required]
        public int LocationID { get; set; }

        public virtual PersonalizedCategory PersonalizedCategory { get; set; }
        public virtual Location Location { get; set; }
    }
}