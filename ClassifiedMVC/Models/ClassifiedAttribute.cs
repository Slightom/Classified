using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class ClassifiedAttribute
    {
        [Required]
        public int ClassifiedAttributeID { get; set; }

        [Required]
        public int ClassifiedID { get; set; }

        [Required]
        public int AttributeID { get; set; }

        public string Value { get; set; }


        public virtual Classified Classified { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}