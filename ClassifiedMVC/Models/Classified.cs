using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class Classified
    {
        [Required]
        public int ClassifiedID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string State { get; set; }


        public int Counter { get; set; }

        public string Reported { get; set; } // if nobody reported string is null



        public string CategoryPath { get; set; }


        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ClassifiedLocation> ClassifiedLocations { get; set; }
        public virtual ICollection<ClassifiedAttribute> ClassifiedAttributes { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}