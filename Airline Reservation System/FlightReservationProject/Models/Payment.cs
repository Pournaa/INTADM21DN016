using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationProject.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int paymentId { get; set; }
        [Display(Name = "Payment Mode")]
        [Required]
        public string paymentMode { get; set; }
        [Display(Name = "Total Amount")]
        public double totalAmount { get; set; }
        [Display(Name = "Bank Details")]
        [Required]
        public string bankDetails { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }

    }
}