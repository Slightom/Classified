using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class AttributeValue
    {
        [Required]
        public int AttributeValueID { get; set; }

        [Required]
        public int AttributeID { get; set; }

        [Required]
        public string Value { get; set; }

        public virtual Attribute Attribute { get; set; }
    }
}