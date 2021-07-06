using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectBusss.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        public int scheduleId { get; set; }
        [ForeignKey("Bus")]
        public int BusId { get; set; }
        [Required]
        public string source { get; set; }
        [Required]
        public int sourceId { get; set; }
        [Required]
        public string dest { get; set; }
        [Required]
        public int destId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime scheduleDate { get; set; }
        [Required]
        public TimeSpan depatureTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime arrivalDate { get; set; }
        [Required]
        public TimeSpan arrivalTime { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public int AC_SleeperSeats { get; set; } //remaining seats in First class
        [Required]
        public int NonAC_SleeperSeats { get; set; } //remaining seats in Business class
        [Required]
        public int ACSemi_SleeperSeats { get; set; } //remaining seats in Economy class
        [Required]
        public int NonACSemi_SleeperSeats { get; set; }
        [Required]
        public double costAC_Sleeper { get; set; }
        [Required]
        public double costNonAC_Sleeper { get; set; }
        [Required]
        public double costACSemi_Sleeper { get; set; }
        [Required]
        public double costNonACSemi_Sleeper { get; set; }
        [Required]
        public string travelType { get; set; }


        public virtual Bus Bus { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }


    }
}