using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmerApp.Models
{
    public class Adminapproval
    {
        [Key]
        public int Adminapp_ID { get; set; }
        public int Farmer_ID { get; set; }
        public int Crop_ID { get; set; }
        public string Status { get; set; }
        public Nullable<int> Bidder_ID { get; set; }
      

    }
}