using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightReservationProject.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Display(Name = "ScheduleId")]
        [Key]
        public int scheduleId { get; set; }
        [ForeignKey("Flight")]
        [Display(Name = "Flight Name")]
        public int flightId { get; set; }
        [Display(Name = "Source")]
        [Required]
        public string source { get; set; }
        [Display(Name = "Source Id")]
        [Required]
        public int sourceId { get; set; }
        [Display(Name = "Destination")]
        [Required]
        public string dest { get; set; }
        [Display(Name = "Destination Id")]
        [Required]
        public int destId { get; set; }
        [Display(Name = "Schedule Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime scheduleDate { get; set; }
        [Display(Name = "Departure Time")]
        [Required]
        public TimeSpan depatureTime { get; set; }
        [Display(Name = "Arrival Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime arrivalDate { get; set; }
        [Display(Name = "Arrival Time")]
        [Required]
        public TimeSpan arrivalTime { get; set; }
        [Display(Name = "Status")]
        [Required]
        public string status { get; set; }
        [Display(Name = "FirstClass Seats")]
        [Required]
        public int FCseats { get; set; } //remaining seats in First class
        [Display(Name = "BuisnessClass Seats")]
        [Required]
        public int BCseats { get; set; } //remaining seats in Business class
        [Display(Name = "EconomyClass Seats")]
        [Required]
        public int ECseats { get; set; } //remaining seats in Economy class
        [Display(Name = "FCCost")]
        [Required]
        public double costFirst { get; set; }
        [Display(Name = "BCCost")]
        [Required]
        public double costBusiness { get; set; }
        [Display(Name = "ECCost")]
        [Required]
        public double costEconomy { get; set; }
        [Display(Name = "TravelType")]
        [Required]
        public string traveltype { get; set; }


        public virtual Flight Flight { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }


    }
}