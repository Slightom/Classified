using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class Category
    {
        [Required]
        public int CategoryID { get; set; }

        public int? CategoryFatherID { get; set; } 

        [Required]
        public string Name { get; set; }


        public virtual Category CategoryFather { get; set; }

        public virtual ICollection<Classified> Classified { get; set; }
        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; }
        public virtual ICollection<PersonalizedCategory> PersonalizedCategories { get; set; }

    }
}