using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Classified.Models
{
    public class Message
    {
        [Required]
        public int MessageID { get; set; }

        [Required]
        public string SenderID { get; set; }

        [Required]
        public string ReceiverID { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public bool Read { get; set; }


        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}