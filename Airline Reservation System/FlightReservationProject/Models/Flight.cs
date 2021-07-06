using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationProject.Models
{
    [Table("Flight")]
    public class Flight
    {
        [Required]
        [Key]
        public int flightId { get; set; }
        [Display(Name = "Flight Name")]
        [Required]
        public string flightName { get; set; }
        [Display(Name = "Seat First Class")]
        [Required]
        public int seatFirstclass { get; set; }
        [Display(Name = "Seat Buisness Class")]
        [Required]
        public int seatBusinessclass { get; set; }
        [Display(Name = "Seat Economy Class")]
        [Required]
        public int seatEconomyclass { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}