using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectBusss.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int paymentId { get; set; }
        public string paymentMode { get; set; }
        public double totalAmount { get; set; }
        public string bankDetails { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }

    }
}