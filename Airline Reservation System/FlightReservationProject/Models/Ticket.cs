using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FlightReservationProject.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        [Display(Name = "TicketId")]
        [Required]
        [Key]
        public int ticketId { get; set; }
        // [ForeignKey("UserId")]
        [Display(Name = "User Name")]
        public string UserId { get; set; }
        //[ForeignKey("UserId")]
        //public virtual ApplicationUser User { get; set; }
        [Display(Name = "Flight Id")]
        [ForeignKey("Flight")]
        public int flightId { get; set; }
        [ForeignKey("Schedule")]
        public int scheduleId { get; set; }
        [ForeignKey("Payment")]
        public int paymentId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Journey")]
        public DateTime dateOfJourney { get; set; }
        [Display(Name = "Seat No")]
        public int seatNo { get; set; }
        [Display(Name = "Passenger Name")]
        [Required]
        public string passengerName { get; set; }
        [Display(Name = "Gender")]
        [Required]
        public char gender { get; set; }
        [Display(Name = "Phonenumber")]
        [Phone]
        [Required]
        public string phoneNumber { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string address { get; set; }
        [Display(Name = "Emergency Contact")]
        [Required]
        public string emergencyContact { get; set; }
        [Display(Name = "Travel Class")]
        [Required]
        public string travelclass { get; set; }
        [Display(Name = "No of Tickets")]
        

        public virtual Flight Flight { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Payment Payment { get; set; }
        //public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}