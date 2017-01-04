using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class Location
    {
        [Required]
        public int LocationID { get; set; }

        [Required]
        public string LocationName { get; set; }

        public virtual ICollection<PCL> PCLs { get; set; }
        public virtual ICollection<ClassifiedLocation> ClassifiedLocations { get; set; }
     }
}