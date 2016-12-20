using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class Attribute
    {
        [Required]
        public int AttributeID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; }
        public virtual ICollection<ClassifiedAttribute> ClassifiedAttributes { get; set; }
        public virtual ICollection<AttributeValue> AttributeValuses { get; set; }
    }
}