using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class CategoryAttribute
    {
        [Required]
        public int CategoryAttributeID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int AttributeID { get; set; }


        public virtual Category Category { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}