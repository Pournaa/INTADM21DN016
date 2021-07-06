using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FlightReservationProject.Models
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public int feedbackId { get; set; }

        public string UserId { get; set; }
        [Display(Name = "User Name")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Feed")]

        [Required]
        [StringLength(250)]

        public string feed { get; set; }




    }
}