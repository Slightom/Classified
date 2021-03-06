﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class Photo
    {
        [Required]
        public int PhotoID { get; set; }

        [Required]
        public int ClassifiedID { get; set; }

        public bool MainPhoto { get; set; }

        public string Path { get; set; }

        public virtual Classified Classified { get; set; }
    }
}