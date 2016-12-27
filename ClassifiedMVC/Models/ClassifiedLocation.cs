using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class ClassifiedLocation
    {
        [Required]
        public int ClassifiedLocationID { get; set; }

        [Required]
        public int ClassifiedID { get; set; }

        [Required]
        public int LocationID { get; set; }

        public virtual Location Location { get; set; }
        public virtual Classified Classified { get; set; }
    }
}