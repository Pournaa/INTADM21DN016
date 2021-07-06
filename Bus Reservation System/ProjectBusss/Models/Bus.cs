using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectBusss.Models
{
    [Table("Bus")]
    public class Bus
    {
        public enum Ac
        {
            Ac,
            Non_Ac
        }
        public enum Sleeper
        {
            Sleeper,
            Semi_Sleeper
        }
        [Required]
        [Key]
        public int BusId { get; set; }
        [Required]
        public string BusName { get; set; }
        [Required]
        public Ac Type_AC { get; set; }
        [Required]
        public Sleeper Type_Sleeper { get; set; }


        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }

    }
}