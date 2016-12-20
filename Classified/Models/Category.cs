using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class Category
    {
        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int CategoryFatherID { get; set; } // 0-null

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Classified> Classified { get; set; }
        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; }

    }
}